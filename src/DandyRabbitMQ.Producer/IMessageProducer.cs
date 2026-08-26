namespace DandyRabbitMQ.Producer;

public interface IMessageProducer
{
    Task<bool> ProduceAsync(string exchange, IEnumerable<string> routingKeys, Guid id, DateTime timestamp, object message, CancellationToken cancellationToken);
}