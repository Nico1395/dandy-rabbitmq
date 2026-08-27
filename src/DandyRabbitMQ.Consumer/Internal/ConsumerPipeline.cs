using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Consumer.Internal;

public class ConsumerPipeline(IServiceProvider serviceProvider) : IConsumerPipeline
{
    public async Task<ConsumerStatus> ExecuteAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : class
    {
        try
        {
            var consumer = serviceProvider.GetRequiredService<IConsumer<TMessage>>();
            var middlewares = serviceProvider.GetServices<IConsumerMiddleware<TMessage>>();

            ConsumerDelegate consumerDelegate = () => consumer.ConsumeAsync(message, cancellationToken);

            foreach (var middleware in middlewares.Reverse())
            {
                var next = consumerDelegate;
                consumerDelegate = () => middleware.InterceptAsync(message, next, cancellationToken);
            }

            return await consumerDelegate.Invoke();
        }
        catch (Exception ex)
        {
            var exceptionHandler = serviceProvider.GetService<IConsumerExceptionHandler<TMessage>>();
            if (exceptionHandler != null)
                await exceptionHandler.HandleAsync(message, ex, cancellationToken);

            throw;
        }
    }
}