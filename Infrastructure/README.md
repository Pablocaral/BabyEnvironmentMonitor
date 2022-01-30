# Infrastructure
This folder contains the resources required for deploy the application on the server node using docker as container orchestrator.

Because of the system is composed by external and internal components, before creating the docker container of each components, you should build the docker images of internal components. You can create this images following the steps detailed on the README files of each project. 

After create this docker images, if you prefer, you can push them to a Docker repository, in order to access it from other work spaces, and be able to deploy the application, without the previous step of building the images, from any machine.

Once the internal component images had been created, you can run all services required by the application executing the follow command:

```bash
    docker-compose up
```

The docker-compose file is configured to allow access to the services that should be acceded by external component, like the broker, which should be acceded by de IoT node to load data obtained by sensor, or the Environment Data Api, that should be acceded by the smartphone application to query the environment data.