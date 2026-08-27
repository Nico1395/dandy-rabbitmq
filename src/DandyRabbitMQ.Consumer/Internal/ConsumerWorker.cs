using DandyRabbitMQ.Consumer.Configuration;
using DandyRabbitMQ.Core.Connectivity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DandyRabbitMQ.Consumer.Internal;

internal sealed class ConsumerWorker(
    ConsumerConfiguration consumerConfiguration,
    IServiceProvider serviceProvider,
    IConnectionProvider connectionProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (consumerConfiguration.Channels == null)
                throw new InvalidOperationException("No queues are configured.");

            var connection = await connectionProvider.GetAsync(stoppingToken);

            foreach (var channelConfiguration in consumerConfiguration.Channels)
            {
                var channel = await connection.CreateChannelAsync(channelConfiguration.Options, stoppingToken);
                await InitializeChannelAsync(channel, channelConfiguration, stoppingToken);
            }
        }
        catch (Exception ex)
        {
            // TODO -> Log
            throw;
        }
    }

    private async Task InitializeChannelAsync(IChannel channel, ChannelConfiguration configuration, CancellationToken cancellationToken)
    {
        var ackLock = new SemaphoreSlim(1, 1);

        await channel.ExchangeDeclareAsync(
            exchange: configuration.Exchange.Name ?? throw new InvalidOperationException("Exchange name is not set."),
            type: configuration.Exchange.ExchangeType,
            durable: configuration.Exchange.Durable,
            autoDelete: configuration.Exchange.AutoDelete,
            cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: configuration.Queue.Name ?? throw new InvalidOperationException("Queue name is not set."),
            durable: configuration.Queue.Durable,
            exclusive: configuration.Queue.Exclusive,
            autoDelete: configuration.Queue.AutoDelete,
            arguments: configuration.Queue.Arguments,
            noWait: configuration.Queue.NoWait,
            cancellationToken);

        await channel.BasicQosAsync(
            prefetchSize: configuration.PrefetchSize,
            prefetchCount: configuration.PrefetchCount,
            global: configuration.Global,
            cancellationToken);

        if (configuration.Queue.RoutingKeys == null)
            throw new InvalidOperationException("Routing keys are not set.");

        foreach (var routingKey in configuration.Queue.RoutingKeys)
        {
            await channel.QueueBindAsync(
                queue: configuration.Queue.Name,
                exchange: configuration.Exchange.Name,
                routingKey: routingKey,
                arguments: configuration.Queue.Arguments,
                noWait: configuration.Queue.NoWait,
                cancellationToken: cancellationToken);
        }
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (_, args) => serviceProvider
            .GetRequiredService<IReceiver>()
            .ReceiveAsync(args, ackLock, channel, configuration, cancellationToken);

        if (string.IsNullOrWhiteSpace(configuration.ConsumerTag))
        {
            await channel.BasicConsumeAsync(
                queue: configuration.Queue.Name,
                autoAck: configuration.AutoAck,
                consumer: consumer,
                cancellationToken: cancellationToken);
        }
        else
        {
            await channel.BasicConsumeAsync(
                queue: configuration.Queue.Name,
                autoAck: configuration.AutoAck,
                consumerTag: configuration.ConsumerTag,
                consumer: consumer,
                cancellationToken: cancellationToken);
        }
    }
}