# What is DandyRabbitMQ?
_DandyRabbitMQ_ is a framework that offers comfort features and facades for the original client API [RabbitMQ.Client](https://github.com/rabbitmq/rabbitmq-dotnet-client) by Broadcom. The goal is to make the use of RabbitMQ.Client significantly easier, avoid boilerplate and streamline communication.

# Setup and packages
DandyRabbitMQ comes with multiple packages. Often frameworks with a large amount of packages can easily confuse. This section's goal is to make the dependency graph of the packages, and more importantly what packages you need in what case, more transparent. Also, this section is supposed to show you how to setup the framework in both consumer and/or producer applications.

## Package dependencies
The following UML package diagram shows the dependency graph of all current packages.
- `DandyRabbitMQ.Core` contains connectivity concerns, message abstractions and attributes for messages.
- `DandyRabbitMQ.Serialization` contains abstractions for serializing a messages payload
- `DandyRabbitMQ.Serialization.SystemTextJson` and `DandyRabbitMQ.Serialization.NewtonsoftJson` contain implementations of the payload serialization abstractions with respective serialization frameworks
- `DandyRabbitMQ.Consumer` contains a message consumption worker and consumer abstractions for consuming messages
- `DandyRabbitMQ.Producer` contains message production and dispatching

![Package Dependency Graph](/docs/package-dependency-graph.drawio.png)

## Packages for producers
Producer **will** need to reference only `DandyRabbitMQ.Consumer` and any of the serialization implementations, so either `DandyRabbitMQ.Serialization.SystemTextJson` or `DandyRabbitMQ.Serialization.NewtonsoftJson`.

This way `DandyRabbitMQ.Core` and thus `DandyRabbitMQ.Serialization` are both automatically included. Also, the producer configuration layer allows to configure both serialization and connectivity, so there is no need to add either to your `IServiceCollection` manually, that is done behind the scenes.

### Setup
## Packages for consumers
Producer **will** need to reference only `DandyRabbitMQ.Producer` and any of the serialization implementations, so either `DandyRabbitMQ.Serialization.SystemTextJson` or `DandyRabbitMQ.Serialization.NewtonsoftJson`.

This way `DandyRabbitMQ.Core` and thus `DandyRabbitMQ.Serialization` are both automatically included. Also, the producer configuration layer allows to configure both serialization and connectivity, so there is no need to add either to your `IServiceCollection` manually, that is done behind the scenes.

### Setup
