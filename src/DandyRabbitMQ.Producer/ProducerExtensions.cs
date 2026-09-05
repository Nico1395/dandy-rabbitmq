using DandyRabbitMQ.Core.Messages;
using RabbitMQ.Client;

namespace DandyRabbitMQ.Producer;

public static class ProducerExtensions
{
    public static Task ProduceAsync(this IProducer producer, string? exchange, IEnumerable<string>? routingKeys, string id, DateTime timestamp, object message, CancellationToken cancellationToken)
    {
        var properties = new BasicProperties
        {
            MessageId = id,
            Timestamp = new AmqpTimestamp(timestamp.Ticks),
        };

        return producer.ProduceAsync(exchange, routingKeys, message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, string routingKey, string id, DateTime timestamp, object message, CancellationToken cancellationToken)
    {
        var properties = new BasicProperties
        {
            MessageId = id,
            Timestamp = new AmqpTimestamp(timestamp.Ticks),
        };

        return producer.ProduceAsync(exchange, [routingKey], message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, IEnumerable<string>? routingKeys, string id, DateTime timestamp, object message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        properties ??= new BasicProperties();

        properties.MessageId = id;
        properties.Timestamp = new AmqpTimestamp(timestamp.Ticks);

        return producer.ProduceAsync(exchange, routingKeys, message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, string routingKey, string id, DateTime timestamp, object message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        properties ??= new BasicProperties();

        properties.MessageId = id;
        properties.Timestamp = new AmqpTimestamp(timestamp.Ticks);

        return producer.ProduceAsync(exchange, [routingKey], message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange: null, routingKeys: null, id.ToString(), timestamp, message, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, IEnumerable<string>? routingKeys, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys, id.ToString(), timestamp, message, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, string routingKey, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKey, id.ToString(), timestamp, message, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, Guid id, DateTime timestamp, object message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange: null, routingKeys: null, id.ToString(), timestamp, message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, IEnumerable<string>? routingKeys, Guid id, DateTime timestamp, object message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys, id.ToString(), timestamp, message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, string routingKey, Guid id, DateTime timestamp, object message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKey, id.ToString(), timestamp, message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, IEnumerable<string>? routingKeys, IMessage message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys, message.Id, message.Timestamp, message, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, string routingKey, IMessage message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKey, message.Id, message.Timestamp, message, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, IMessage message, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(message.Id, message.Timestamp, message, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, IEnumerable<string>? routingKeys, IMessage message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKeys, message.Id, message.Timestamp, message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, string? exchange, string routingKey, IMessage message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(exchange, routingKey, message.Id, message.Timestamp, message, properties, cancellationToken);
    }

    public static Task ProduceAsync(this IProducer producer, IMessage message, BasicProperties? properties, CancellationToken cancellationToken)
    {
        return producer.ProduceAsync(message.Id, message.Timestamp, message, properties, cancellationToken);
    }
}