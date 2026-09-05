namespace DandyRabbitMQ.Consumer;

public interface IConsumer<in TMessage>
    where TMessage : class
{
    Task<ConsumerResult> ConsumeAsync(TMessage message, ConsumerContext context, CancellationToken cancellationToken);
}