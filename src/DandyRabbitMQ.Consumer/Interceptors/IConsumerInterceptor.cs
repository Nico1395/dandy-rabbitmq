using RabbitMQ.Client.Events;

namespace DandyRabbitMQ.Consumer.Interceptors;

public interface IConsumerInterceptor
{
    Task OnAfterAckAsync(BasicDeliverEventArgs args, object message, ConsumerResult result, CancellationToken cancellationToken);
    Task OnAfterNackAsync(BasicDeliverEventArgs args, object message, ConsumerResult result, CancellationToken cancellationToken);
}