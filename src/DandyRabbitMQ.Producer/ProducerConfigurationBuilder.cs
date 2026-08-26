using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Producer;

public sealed class ProducerConfigurationBuilder
{
    private readonly ProducerConfiguration _configuration = new();

    public ProducerConfigurationBuilder UseTypeNameFactory(Func<object, string> typeNameFactory)
    {
        _configuration.TypeNameFactory = typeNameFactory;
        return this;
    }

    public Action<SerializationConfigurationBuilder>? Serialization { get; set; }
    public Action<ConnectivityConfigurationBuilder>? Connectivity { get; set; }

    internal ProducerConfiguration Build()
    {
        _configuration.SerializationConfigurationBuilderAction = Serialization;
        _configuration.ConnectivityConfigurationBuilderAction = Connectivity;

        return _configuration;
    }
}