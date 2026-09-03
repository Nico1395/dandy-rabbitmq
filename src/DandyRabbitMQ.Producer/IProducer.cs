namespace DandyRabbitMQ.Producer;

public interface IProducer
{
    Task<bool> ProduceAsync(string exchange, IEnumerable<string> routingKeys, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken);
}