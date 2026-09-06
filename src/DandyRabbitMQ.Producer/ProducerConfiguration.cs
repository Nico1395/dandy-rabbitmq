using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Declarations.Configuration;
using DandyRabbitMQ.Core.Encoding.Configuration;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Producer;

/// <summary>
/// Stores the configuration used to register producer services.
/// </summary>
public sealed class ProducerConfiguration
{
    internal SerializationConfigurationBuilder SerializationConfigurationBuilder { get; set; } = new();
    internal ConnectivityConfigurationBuilder ConnectivityConfigurationBuilder { get; set; } = new();
    internal MessagesConfigurationBuilder MessagesConfigurationBuilder { get; set; } = new();
    internal EncodingConfigurationBuilder EncodingConfigurationBuilder { get; set; } = new();
    internal DeclarationsConfigurationBuilder DeclarationsConfiguration { get; set; } = new();
}