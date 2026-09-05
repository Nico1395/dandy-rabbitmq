namespace DandyRabbitMQ.Consumer;

public interface IConsumerExceptionHandler<in TMessage>
{
    Task HandleAsync(TMessage message, ConsumerContext context, Exception exception, CancellationToken cancellationToken);
}