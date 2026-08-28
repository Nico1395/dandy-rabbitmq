using RabbitMQ.Client.Events;

namespace DandyRabbitMQ.Consumer.Interceptors;

public abstract class ConsumerInterceptor : IConsumerInterceptor
{
    public Task OnAfterAckAsync(BasicDeliverEventArgs args, object message, ConsumerResult result, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task OnAfterNackAsync(BasicDeliverEventArgs args, object message, ConsumerResult result, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}