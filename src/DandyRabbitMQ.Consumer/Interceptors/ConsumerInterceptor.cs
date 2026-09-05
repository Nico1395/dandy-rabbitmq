namespace DandyRabbitMQ.Consumer.Interceptors;

public abstract class ConsumerInterceptor : IConsumerInterceptor
{
    public Task OnAfterAckAsync(object message, ConsumerContext context, ConsumerResult result, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task OnAfterNackAsync(object message, ConsumerContext context, ConsumerResult result, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}