#Environment Data API
This REST API provides access to the baby environment data, given its environment identifier, which had been obtained by the IoT node.

The API only implements GET operations, divided on two main classes:
* State: Returns the current state of the baby environment, composed by the last data recorded, captured by each sensor of the IoT node.
* History: This operations returns the historical data of each sensor, composed by data from the last 7 days.

Once API is started, you can connect to http://localhost:5000/swagger and access to the API swagger definition. This definition contains information about each API operations, with request parameters and expected responses. The swagger page could be used to execute request above the API too.

## Create docker image
You can build and publish the service, and create a docker image using the follow commands:
```bash
    dotnet publish -c Release -o ./publish -f netcoreapp3.1
    docker build . -t environment_data_api
```