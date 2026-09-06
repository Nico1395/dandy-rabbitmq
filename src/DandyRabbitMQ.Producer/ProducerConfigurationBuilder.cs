using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Declarations.Configuration;
using DandyRabbitMQ.Core.Encoding.Configuration;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Producer;

/// <summary>
/// Builds the configuration for DandyRabbitMQ producer services.
/// </summary>
public sealed class ProducerConfigurationBuilder
{
    private readonly ProducerConfiguration _configuration = new();
    /// <summary>
    /// Gets or sets the payload serialization configuration.
    /// </summary>
    public SerializationConfigurationBuilder Serialization { get; set; } = new();

    /// <summary>
    /// Gets or sets the RabbitMQ connectivity configuration.
    /// </summary>
    public ConnectivityConfigurationBuilder Connectivity { get; set; } = new();

    /// <summary>
    /// Gets or sets the message metadata configuration.
    /// </summary>
    public MessagesConfigurationBuilder Messages { get; set; } = new();

    /// <summary>
    /// Gets or sets the payload encoding configuration.
    /// </summary>
    public EncodingConfigurationBuilder Encoding { get; set; } = new();

    /// <summary>
    /// Gets or sets the exchange, queue, and binding declaration configuration.
    /// </summary>
    public DeclarationsConfigurationBuilder Declarations { get; set; } = new();

    internal ProducerConfiguration Build()
    {
        _configuration.SerializationConfigurationBuilder = Serialization;
        _configuration.ConnectivityConfigurationBuilder = Connectivity;
        _configuration.MessagesConfigurationBuilder = Messages;
        _configuration.EncodingConfigurationBuilder = Encoding;
        _configuration.DeclarationsConfiguration = Declarations;

        return _configuration;
    }
}
