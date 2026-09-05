using DandyRabbitMQ.Consumer.Configuration;
using RabbitMQ.Client.Events;

namespace DandyRabbitMQ.Consumer;

public sealed class ConsumerContext
{
    internal ConsumerContext(
        BasicDeliverEventArgs deliverArgs,
        ChannelConfiguration channelConfiguration)
    {
        DeliverArgs = deliverArgs;
        ChannelConfiguration = channelConfiguration;
    }

    public BasicDeliverEventArgs DeliverArgs { get; }
    public ChannelConfiguration ChannelConfiguration { get; }
}