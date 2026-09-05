namespace DandyRabbitMQ.Consumer;

public interface IConsumer<in TMessage>
{
    Task<ConsumerResult> ConsumeAsync(TMessage message, ConsumerContext context, CancellationToken cancellationToken);
}