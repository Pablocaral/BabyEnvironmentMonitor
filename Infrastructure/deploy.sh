dotnet publish -c Release -o ../Source/AlertService/publish -f netcoreapp3.1
docker build ../Source/AlertService -t alert_service

dotnet publish -c Release -o ../Source/BabyEnvironmentMonitor/publish -f netcoreapp3.1
docker build ../Source/BabyEnvironmentMonitor -t environment_data_api

docker-compose up -d