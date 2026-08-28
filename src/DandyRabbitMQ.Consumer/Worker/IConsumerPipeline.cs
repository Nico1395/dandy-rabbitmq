namespace DandyRabbitMQ.Consumer.Worker;

public interface IConsumerPipeline
{
    Task<ConsumerResult> ExecuteAsync<TMessage>(TMessage message, CancellationToken cancellationToken) where TMessage : class;
}