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

    public SerializationConfigurationBuilder Serialization { get; set; } = new();
    public ConnectivityConfigurationBuilder Connectivity { get; set; } = new();

    internal ProducerConfiguration Build()
    {
        _configuration.SerializationConfigurationBuilder = Serialization;
        _configuration.ConnectivityConfigurationBuilder = Connectivity;

        return _configuration;
    }
}