using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Consumer.Worker;

public class ConsumerPipeline(IServiceProvider serviceProvider) : IConsumerPipeline
{
    public async Task<ConsumerResult> ExecuteAsync<TMessage>(TMessage message, ConsumerContext context, CancellationToken cancellationToken)
        where TMessage : class
    {
        try
        {
            var consumer = serviceProvider.GetRequiredService<IConsumer<TMessage>>();
            var middlewares = serviceProvider.GetServices<IConsumerMiddleware<TMessage>>();

            ConsumerDelegate consumerDelegate = () => consumer.ConsumeAsync(message, context, cancellationToken);

            foreach (var middleware in middlewares.Reverse())
            {
                var next = consumerDelegate;
                consumerDelegate = () => middleware.InterceptAsync(message, context, next, cancellationToken);
            }

            return await consumerDelegate.Invoke();
        }
        catch (Exception exception)
        {
            var exceptionHandler = serviceProvider.GetService<IConsumerExceptionHandler<TMessage>>();
            if (exceptionHandler != null)
                await exceptionHandler.HandleAsync(message, context, exception, cancellationToken);

            throw;
        }
    }
}