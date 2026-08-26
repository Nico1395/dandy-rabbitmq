using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Producer;

public sealed class ProducerConfiguration
{
    public Func<object, string>? TypeNameFactory { get; internal set; }

    public Action<SerializationConfigurationBuilder>? SerializationConfigurationBuilderAction { get; internal set; }
    public Action<ConnectivityConfigurationBuilder>? ConnectivityConfigurationBuilderAction { get; internal set; }
}