#include <Arduino.h>
#include <analogWrite.h>
#include <DHT.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include <WiFi.h>
#include <HTTPClient.h>
#include <PubSubClient.h>
#include "driver/periph_ctrl.h"
#include "driver/timer.h"

#define btn1 D2
#define btn2 D3
#define ledRed D12
#define ledBlue D13
#define ADC_MODE2
#define rotation A0
#define temperature D4

//Acceso a la red
#define TOPIC "esi/prototype"
#define TOPIC_NOISE "esi/prototype/noise"
#define TOPIC_TEMPERATURE "esi/prototype/temperature"
#define TOPIC_HUMIDITY "esi/prototype/humidity"
#define TOPIC_LIGHT "esi/prototype/light"
#define TOPIC_CO2 "esi/prototype/co2"

#define NAME "CunaClient"
#define BROKER_IP "192.168.1.40"
#define BROKER_PORT 2883

const char* ssid = "wlan";
const char* password =  "password";

WiFiClient espClient;
PubSubClient client(espClient);

DHT dht(temperature, DHT11);

xTaskHandle xTask5Handle;
xTaskHandle xTask30Handle;
xTaskHandle xTaskSendHandle;
hw_timer_t * timer = NULL;
int cnt_time = 0;

char *_id = "1";
float val_temperature = 0;
float val_humidity = 0;
float val_light = 0;
float val_sound = 0;
float val_co2 = 0;

//conexion //////////////////////////////////////////////////////////////////////////////////////
void wifiConnect()
{
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi..");
  }

  Serial.println("Connected to the WiFi network");
  Serial.print("IP Address: ");
  Serial.println(WiFi.localIP());
}

void mqttConnect() {
  client.setServer(BROKER_IP, BROKER_PORT);
  while (!client.connected()) {
    Serial.print("MQTT connecting ...");

    if (client.connect(NAME)) {
      Serial.println("connected");
    } else {
      Serial.print("failed, status code =");
      Serial.print(client.state());
      Serial.println("try again in 5 seconds");

      delay(5000);  //* Wait 5 seconds before retrying
    }
  }
}

//control de errores ////////////////////////////////////////////////////////////////////////////
void err_handle(){
  digitalWrite(ledRed, 1);
}
void good_handle(){
  digitalWrite(ledRed, 0);
}
void(* resetFunc) (void) = 0; //declare reset function @ address 0
//sondas ///////////////////////////////////////////////////////////////////////////////////////
void getSound(){
  //los rangos de temperatura [0,50] y humedad [20,90] se multiplican para obtener un rango [0,4500]
  //despues se recorta el rango [0,4500] a [0,100]
  float s = dht.readTemperature() * dht.readHumidity() / 45;

  if(s>20){
      err_handle();
    }
    
    if(s>0)
        val_sound = s;
}
void getLight(){
  //los rangos de temperatura [0,50] y humedad [20,90] se multiplican para obtener un rango [0,4500]
  //despues se recorta el rango [0,4500] a [0,100]
  float l = dht.readTemperature() * dht.readHumidity() / 45;
    
    if(l>20){
      err_handle();
    }
    
    if(l>0)
        val_light = l;


}
void getTemperature(){
    float t = dht.readTemperature();
    //control
    if(t<10 || t>35){
      err_handle();
      //Serial.printf("Bad result in temperature: %.2f\n",t);
    }
    
    if(t>0)
        val_temperature = t;

    //Serial.printf("temperature: %.2f \n",t);

}
void getHumidity(){
    float h = dht.readHumidity();

    //control
    if(h<30 || h>90){
      err_handle();
      //Serial.printf("Bad result in humidity %.2f \n",h);
    }
    if(h>0)
        val_humidity = h;
    //Serial.printf("humidity %.2f\n",h);
}
void getCO2(){
  //el rango de temperatura [0,50] pasa a ser [0,100]
    float c = dht.readTemperature() * 2;

    if(c>10){
      err_handle();
      //Serial.printf("Bad result in co2 %.2f \n",c);
    }
    if(c>0)
        val_co2 = c;
    //Serial.printf("co2 %.2f\n",c);
}

//tareas //////////////////////////////////////////////////////////////////////////////////////
void vTaskFunction( void *pvParameters )
{
  char *pcTaskName;
  pcTaskName = ( char * ) pvParameters;
  for(;;) {
    printf(pcTaskName);
    vTaskDelay(1000/portTICK_PERIOD_MS);
  }
  vTaskDelete(NULL);
}

void vTask5(void* pvParam)
{
  for(;;) {

    Serial.printf("Task 5  t:%ld\n",millis());
    good_handle();
    getSound();
    getTemperature();

    vTaskSuspend( NULL); // dejo de trabajar

  }
  vTaskDelete(NULL);
}
void vTask30(void* pvParam)
{
  for(;;) {

    Serial.printf("Task 30 t:%ld\n",millis());
    good_handle();
    getSound();
    getLight();
    getTemperature();
    getHumidity();
    getCO2();

    vTaskSuspend( NULL); // dejo de trabajar

  }
  vTaskDelete(NULL);
}

void vTaskSend(void* pvParam)
{
    
  String jsonData;
  Serial.printf("send task\n");
  if (cnt_time == 30){

      cnt_time = 0;

      jsonData = "{\"environmentId\":"+String(_id)+",\"light\":"+String(val_light)+"}";
      client.publish(TOPIC_LIGHT, jsonData.c_str()); 

      jsonData = "{\"environmentId\":"+String(_id)+",\"humidity\":"+String(val_humidity)+"}";
      client.publish(TOPIC_NOISE, jsonData.c_str()); 

      jsonData = "{\"environmentId\":"+String(_id)+",\"co2\":"+String(val_co2)+"}";
      client.publish(TOPIC_CO2, jsonData.c_str()); 

  }
  if(cnt_time == 0 || cnt_time%5 == 0){

      jsonData = "{\"environmentId\":"+String(_id)+",\"noise\":"+String(val_sound)+"}";
      client.publish(TOPIC_NOISE, jsonData.c_str()); 

      jsonData = "{\"environmentId\":"+String(_id)+",\"temperature\":"+String(val_temperature)+"}";
      client.publish(TOPIC_TEMPERATURE, jsonData.c_str());
    
  }
  
  vTaskDelay(2000/portTICK_PERIOD_MS);
  vTaskDelete(NULL);
}
//tarea de control
void vTaskControl(void* pvParam)
{
  for(;;) {
    
    Serial.printf("control init t:%ld \n",millis());
    for(int i=0;i<5;i++){
      sleep(3);
      vTaskResume( xTask5Handle); // desbloqueo
    }
    vTaskResume( xTask30Handle); // desbloqueo

    Serial.printf("control end t:%ld\n",millis());

  }
  vTaskDelete(NULL);
}

//interrupciones ////////////////////////////////////////////////////////////////////////////
void IRAM_ATTR timeout_handler()
{
  cnt_time++; //contador de interrupciones
  xTaskCreatePinnedToCore(vTaskSend, "Task Send", 10000, NULL, 4, &xTaskSendHandle,0);
}
void IRAM_ATTR on_handleInterrupt(){
  noInterrupts();
  //reset
  resetFunc();
  
  interrupts();
}

void app_main(void)
{
  //interrupciones
  attachInterrupt(digitalPinToInterrupt(btn1), &on_handleInterrupt, FALLING);
  attachInterrupt(digitalPinToInterrupt(btn2), &on_handleInterrupt, FALLING); 

  timer = timerBegin(0, 10000, true);
  timerAttachInterrupt(timer, &timeout_handler, true);
  timerAlarmWrite(timer, 10000, true);
  timerAlarmEnable(timer);
  
  //iniciacion de las tareas
  if(xTaskGetSchedulerState()==taskSCHEDULER_RUNNING)
  Serial.printf("Scheduler is running\n");
  
  xTaskCreatePinnedToCore(vTaskControl, "Task Control", 20000, NULL, 2, NULL,0);
  
  xTaskCreatePinnedToCore(vTask5, "Task 5", 20000, NULL, 1, &xTask5Handle,0);
  xTaskCreatePinnedToCore(vTask30, "Task 30", 20000, NULL, 1, &xTask30Handle,0);
  

}

void setup() {

  Serial.begin(9600);
  pinMode(ledRed, OUTPUT);
  pinMode(ledBlue, OUTPUT);
  pinMode(btn1, INPUT_PULLUP);
  pinMode(btn2, INPUT_PULLUP);
  pinMode(rotation, INPUT);
  pinMode(temperature, INPUT);
  analogReadResolution(10);
  dht.begin();

  delay(4000);
  wifiConnect();
  mqttConnect();
  
  app_main();
}

void loop() {
}
