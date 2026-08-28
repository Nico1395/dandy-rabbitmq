namespace DandyRabbitMQ.Consumer;

public interface IConsumerExceptionHandler<in TMessage>
    where TMessage : class
{
    Task HandleAsync(TMessage message, Exception exception, CancellationToken cancellationToken);
}