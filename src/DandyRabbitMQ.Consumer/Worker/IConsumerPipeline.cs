namespace DandyRabbitMQ.Consumer.Worker;

public interface IConsumerPipeline
{
    Task<ConsumerStatus> ExecuteAsync<TMessage>(TMessage message, CancellationToken cancellationToken) where TMessage : class;
}