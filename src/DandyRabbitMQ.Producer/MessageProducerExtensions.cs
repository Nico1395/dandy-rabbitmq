using DandyRabbitMQ.Core.Messages;

namespace DandyRabbitMQ.Producer;

public static class MessageProducerExtensions
{
    public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, IEnumerable<string> routingKeys, Guid id, object message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys, id, timestamp: DateTime.UtcNow, message, cancellationToken);
    }

    public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, string routingKey, Guid id, object message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys: [routingKey], id, timestamp: DateTime.UtcNow, message, cancellationToken);
    }

    public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, IEnumerable<string> routingKeys, object message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys, id: Guid.NewGuid(), message, cancellationToken);
    }

    public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, string routingKey, object message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys: [routingKey], id: Guid.NewGuid(), message, cancellationToken);
    }

    public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, IEnumerable<string> routingKeys, IMessage message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys, message.Id, message.Timestamp, message, cancellationToken);
    }

    public static Task<bool> ProduceAsync(this IMessageProducer producer, string exchange, string routingKey, IMessage message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys: [routingKey], message.Id, message.Timestamp, message, cancellationToken);
    }
}