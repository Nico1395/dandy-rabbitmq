namespace DandyRabbitMQ.Consumer.Interceptors;

public interface IConsumerInterceptor
{
    Task OnAfterAckAsync(object message, ConsumerContext context, ConsumerResult result, CancellationToken cancellationToken);
    Task OnAfterNackAsync(object message, ConsumerContext context, ConsumerResult result, CancellationToken cancellationToken);
}