using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using DandyRabbitMQ.Consumer.Configuration;
using DandyRabbitMQ.Consumer.Interceptors;
using DandyRabbitMQ.Core.Messages.Types;
using DandyRabbitMQ.Serialization;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DandyRabbitMQ.Consumer.Worker;

public class Receiver(
    ConsumerConfiguration consumerConfiguration,
    IServiceProvider serviceProvider,
    IConsumerPipeline consumerPipeline,
    IPayloadSerializer payloadSerializer) : IReceiver
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> _executeAsync = [];

    public async Task ReceiveAsync(BasicDeliverEventArgs args, SemaphoreSlim ackLock, IChannel channel, ChannelConfiguration configuration, CancellationToken cancellationToken)
    {
        // Assume failure
        var result = ConsumerResult.Nack();
        object? message = null;

        try
        {
            var messageType = args.BasicProperties.Type != null ? MessageTypeNameMap.GetType(args.BasicProperties.Type) : null;
            if (messageType == null)
                throw new InvalidOperationException("Failed to resolve message type.");

            var json = Encoding.UTF8.GetString(args.Body.Span);
            if (messageType == null || string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException("Failed to deserialize message.");

            message = payloadSerializer.Deserialize(json, messageType);
            using var scope = serviceProvider.CreateScope();
            {
                var executeAsync = GetExecuteAsync(messageType);
                if (executeAsync.Invoke(consumerPipeline, parameters: [message, cancellationToken]) is not Task<ConsumerResult> task)
                    throw new InvalidOperationException($"Failed to handle '{messageType}'.");

                result = await task;
            }
        }
        catch (Exception ex)
        {
            consumerConfiguration.OnExceptionWhenReceivingMessage?.Invoke(serviceProvider, ex);
        }

        await AckOrNackAsync(args, ackLock, channel, result, cancellationToken);
        await InterceptAckOrNackAsync(args, message, result, cancellationToken);
    }

    private static MethodInfo GetExecuteAsync(Type messageType)
    {
        return _executeAsync.GetOrAdd(messageType, type =>
        {
            var executeAsync = typeof(IConsumerPipeline).GetMethod(nameof(IConsumerPipeline.ExecuteAsync))?.MakeGenericMethod(type);
            if (executeAsync == null)
                throw new UnreachableException($"Failed to reflect method '{nameof(IConsumerPipeline.ExecuteAsync)}' from '{nameof(IConsumerPipeline)}'.");

            return executeAsync;
        });
    }

    private async Task AckOrNackAsync(BasicDeliverEventArgs args, SemaphoreSlim ackLock, IChannel channel, ConsumerResult result, CancellationToken cancellationToken)
    {
        try
        {
            // Using a lock so the channel can be used safely in multiple threads. Intentionally avoiding the cancellation token,
            // because when stopping the ack or nack should still go through to avoid message noise in the queue.
            await ackLock.WaitAsync(cancellationToken: CancellationToken.None);

            // Evaluate and act accordingly
            if (result.Status == ConsumerStatus.Ack)
            {
                await channel.BasicAckAsync(
                    args.DeliveryTag,
                    multiple: result.Multiple,
                    cancellationToken);
            }
            else
            {
                await channel.BasicNackAsync(
                    args.DeliveryTag,
                    multiple: result.Multiple,
                    requeue: result.Requeue,
                    cancellationToken);
            }
        }
        catch (Exception ex)
        {
            consumerConfiguration.OnExceptionWhenAckOrNack?.Invoke(serviceProvider, ex);
        }
        finally
        {
            ackLock.Release();
        }
    }

    private async Task InterceptAckOrNackAsync(BasicDeliverEventArgs args, object? message, ConsumerResult result, CancellationToken cancellationToken)
    {
        if (message == null)
            return;

        try
        {
            var interceptor = serviceProvider.GetService<IConsumerInterceptor>();
            if (interceptor == null)
                return;

            if (result.Status == ConsumerStatus.Ack)
                await interceptor.OnAfterAckAsync(args, message, result, cancellationToken);
            else
                await interceptor.OnAfterNackAsync(args, message, result, cancellationToken);
        }
        catch (Exception ex)
        {
            consumerConfiguration.OnExceptionWhenIntercepting?.Invoke(serviceProvider, ex);
        }
    }
}