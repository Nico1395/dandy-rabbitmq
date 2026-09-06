using RabbitMQ.Client;

namespace DandyRabbitMQ.Core.Declarations;

public interface IDeclarer
{
    Task DeclareQueueAsync(string queueName, IChannel? channel, CancellationToken cancellationToken);
}