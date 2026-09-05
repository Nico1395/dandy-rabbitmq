namespace DandyRabbitMQ.Consumer;

public interface IConsumerMiddleware<in TMessage>
{
    Task<ConsumerResult> InterceptAsync(TMessage message, ConsumerContext context, ConsumerDelegate nextStep, CancellationToken cancellationToken);
}