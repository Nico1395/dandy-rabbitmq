using System.Collections.Concurrent;

namespace DandyRabbitMQ.Core.Declarations.Configuration;

public sealed class DeclarationsConfiguration
{
    internal ConcurrentDictionary<string, ChannelConfiguration> Channels { get; } = new();

    public IReadOnlyDictionary<string, ChannelConfiguration> ChannelsByQueueName => Channels;
}