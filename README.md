# BabyEnvironmentMonitor

Aquí se puede encontrar todo el código para la ejecución del API para obtener la información obtenida del nodo que se ha insertado en la base de datos de influxDB. Para poder lanzar los servicios, se cuenta en el directorio Infrastructure con un script de bash para poder lanzar todo a través de docker. Bien se puede ir al propio directorio o bien se puede ejecutar el script desde este directorio con los siguientes comandos:

You can build and publish the service, and create a docker image using the follow commands:
```bash
    ./deploy.sh
    ./Infrastructure/deploy.sh
```
IMPORTANTE!!!! #############################################################################################################
Dentro de cada directorio de los servicios, se cuenta con un README que específica como poder lanzar a "mano" los servicios.

Repositorio:
https://github.com/Pablocaral/BabyEnvironmentMonitor
