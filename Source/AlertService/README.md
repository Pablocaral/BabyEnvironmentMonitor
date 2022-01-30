# Alert Service
This is a simple service which search the last registry of the baby environment data and analyze the values read by sensors. If any of this data is out of a range, considered the optimum range of the data for the baby health, the service generate an alert.

Alerts could be of type medium, if the value is out of the optimist range, but not with a high difference, and could be of type high, if the value is out of optimum range with a high difference.

The service define a notification interface: INotifier, which can be implemented by multiples classes in order to define different notification channels. Currently, for the prototype, only exists one implementation of this interface, which notify using the output console, but on a real case, another notifier would be implemented to sent alerts to the smartphone application.

## Create docker image
You can build and publish the service, and create a docker image using the follow commands:
```bash
    dotnet publish -c Release -o ./publish -f netcoreapp3.1
    docker build . -t alert_service
```