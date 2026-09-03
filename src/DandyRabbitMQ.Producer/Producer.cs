using System.Text;
using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;
using RabbitMQ.Client;

namespace DandyRabbitMQ.Producer;

internal sealed class Producer(
    IPayloadSerializer payloadSerializer,
    IConnectionProvider connectionProvider,
    MessagesConfiguration messagesConfiguration) : IProducer
{
    private IChannel? _channel;

    public async Task<bool> ProduceAsync(string exchange, IEnumerable<string> routingKeys, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
    {
        var filteredRoutingKeys = routingKeys.Where(key => !string.IsNullOrWhiteSpace(key)).Distinct().ToArray();
        if (filteredRoutingKeys.Length == 0)
            return false;

        if (string.IsNullOrWhiteSpace(exchange))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(exchange));

        if (!messagesConfiguration.MessagesByRuntimeType.TryGetValue(message.GetType(), out var messageConfiguration))
            throw new InvalidOperationException($"Failed to resolve message configuration for type '{message.GetType()}'.");

        var properties = new BasicProperties
        {
            Type = messageConfiguration.Key,
            MessageId = id.ToString(),
            Timestamp = new AmqpTimestamp(timestamp.Ticks),
        };
        var json = payloadSerializer.Serialize(message, messageConfiguration.RuntimeType);
        var channel = await GetChannelAsync(cancellationToken);

        foreach (var routingKey in filteredRoutingKeys)
        {
            await channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: properties,
                body: Encoding.UTF8.GetBytes(json),
                cancellationToken: cancellationToken);
        }

        return true;
    }

    private async Task<IChannel> GetChannelAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
            return _channel;

        var connection = await connectionProvider.GetAsync(cancellationToken);
        return _channel = await connection.CreateChannelAsync(options: null, cancellationToken);
    }
}