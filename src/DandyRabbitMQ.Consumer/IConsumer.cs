namespace DandyRabbitMQ.Consumer;

public interface IConsumer<in TMessage>
    where TMessage : class
{
    Task<ConsumerStatus> ConsumeAsync(TMessage message, CancellationToken cancellationToken);
}