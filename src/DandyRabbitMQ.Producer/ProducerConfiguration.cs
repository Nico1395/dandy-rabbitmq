using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Producer;

public sealed class ProducerConfiguration
{
    public Func<object, string>? TypeNameFactory { get; internal set; }

    public SerializationConfigurationBuilder SerializationConfigurationBuilder { get; internal set; } = new();
    public ConnectivityConfigurationBuilder ConnectivityConfigurationBuilder { get; internal set; } = new();
    public MessagesConfigurationBuilder MessagesConfigurationBuilder { get; internal set; } = new();
}