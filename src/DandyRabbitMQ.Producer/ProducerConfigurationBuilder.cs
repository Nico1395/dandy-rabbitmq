using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Producer;

public sealed class ProducerConfigurationBuilder
{
    private readonly ProducerConfiguration _configuration = new();

    public SerializationConfigurationBuilder Serialization { get; set; } = new();
    public ConnectivityConfigurationBuilder Connectivity { get; set; } = new();
    public MessagesConfigurationBuilder Messages { get; set; } = new();

    internal ProducerConfiguration Build()
    {
        _configuration.SerializationConfigurationBuilder = Serialization;
        _configuration.ConnectivityConfigurationBuilder = Connectivity;
        _configuration.MessagesConfigurationBuilder = Messages;

        return _configuration;
    }
}