namespace DandyRabbitMQ.Consumer.Worker;

public interface IConsumerPipeline
{
    Task<ConsumerResult> ExecuteAsync<TMessage>(TMessage message, ConsumerContext context, CancellationToken cancellationToken) where TMessage : class;
}