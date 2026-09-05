using RabbitMQ.Client;

namespace DandyRabbitMQ.Producer;

public interface IProducer
{
    Task ProduceAsync(string? exchange, IEnumerable<string>? routingKeys, object message, BasicProperties? properties, CancellationToken cancellationToken);
}