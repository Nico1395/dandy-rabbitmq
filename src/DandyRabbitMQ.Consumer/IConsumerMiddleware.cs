namespace DandyRabbitMQ.Consumer;

public interface IConsumerMiddleware<in TMessage>
    where TMessage : class
{
    Task<ConsumerResult> InterceptAsync(TMessage message, ConsumerContext context, ConsumerDelegate nextStep, CancellationToken cancellationToken);
}