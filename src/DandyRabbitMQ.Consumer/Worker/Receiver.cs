using System.Reflection;
using System.Text;
using DandyRabbitMQ.Consumer.Configuration;
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
    public async Task ReceiveAsync(BasicDeliverEventArgs args, SemaphoreSlim ackLock, IChannel channel, ChannelConfiguration configuration, CancellationToken cancellationToken)
    {
        // Assume failure
        var status = ConsumerStatus.Nack;

        try
        {
            var messageType = args.BasicProperties.Type != null ? MessageTypeNameMap.GetType(args.BasicProperties.Type) : null;
            if (messageType == null)
                throw new InvalidOperationException("Failed to resolve message type.");

            MethodInfo executeAsync = null!;
            if (executeAsync == null)
                throw new InvalidOperationException($"Failed to reflect method '{nameof(IConsumer<>.ConsumeAsync)}' from '{typeof(IConsumer<>).MakeGenericType(messageType)}'.");

            var json = Encoding.UTF8.GetString(args.Body.Span);
            if (messageType == null || string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException("Failed to deserialize message.");

            var message = payloadSerializer.Deserialize(json, messageType);
            using var scope = serviceProvider.CreateScope();
            {
                if (executeAsync.Invoke(consumerPipeline, parameters: [message, cancellationToken]) is not Task<ConsumerStatus> task)
                    throw new InvalidOperationException($"Failed to handle '{messageType}'.");

                status = await task;
            }
        }
        catch (Exception ex)
        {
            consumerConfiguration.OnExceptionWhenReceivingMessage?.Invoke(serviceProvider, ex);
        }

        // Using a lock so the channel can be used safely in multiple threads
        // Intentionally avoiding the cancellation token, because when stopping the ack or nack should still go through to avoid message noise in the queue.
        await ackLock.WaitAsync(cancellationToken: CancellationToken.None);

        try
        {
            // Evaluate and act accordingly
            if (status == ConsumerStatus.Ack)
            {
                await channel.BasicAckAsync(args.DeliveryTag, multiple: false, cancellationToken);
            }
            else
            {
                // TODO -> Allow configuration of requeue and multiple
                await channel.BasicNackAsync(
                    args.DeliveryTag,
                    multiple: false,
                    requeue: false,
                    cancellationToken);
            }
        }
        finally
        {
            ackLock.Release();
        }
    }
}