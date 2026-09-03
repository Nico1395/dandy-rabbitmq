using System.Text;
using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Core.Messages.Types;
using DandyRabbitMQ.Serialization;
using RabbitMQ.Client;

namespace DandyRabbitMQ.Producer;

internal sealed class MessageProducer(
    IPayloadSerializer payloadSerializer,
    IConnectionProvider connectionProvider,
    MessagesConfiguration messagesConfiguration,
    ProducerConfiguration producerConfiguration) : IMessageProducer
{
    private IChannel? _channel;

    public async Task<bool> ProduceAsync(string exchange, IEnumerable<string> routingKeys, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
    {
        var filteredRoutingKeys = routingKeys.Where(key => !string.IsNullOrWhiteSpace(key)).Distinct().ToArray();
        if (filteredRoutingKeys.Length == 0)
            return false;

        if (string.IsNullOrWhiteSpace(exchange))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(exchange));

        var properties = new BasicProperties
        {
            Type = messagesConfiguration.MessagesByRuntimeType[message.GetType()].Type,
            MessageId = id.ToString(),
            Timestamp = new AmqpTimestamp(timestamp.Ticks),
        };
        var json = payloadSerializer.Serialize(message);
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