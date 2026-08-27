namespace DandyRabbitMQ.Consumer;

public interface IConsumerMiddleware<in TMessage>
    where TMessage : class
{
    Task<ConsumerStatus> InterceptAsync(TMessage message, ConsumerDelegate nextStep, CancellationToken cancellationToken);
}