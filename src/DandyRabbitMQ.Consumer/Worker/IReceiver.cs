using DandyRabbitMQ.Consumer.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DandyRabbitMQ.Consumer.Worker;

public interface IReceiver
{
    Task ReceiveAsync(BasicDeliverEventArgs args, SemaphoreSlim ackLock, IChannel channel, ChannelConfiguration configuration, CancellationToken cancellationToken);
}