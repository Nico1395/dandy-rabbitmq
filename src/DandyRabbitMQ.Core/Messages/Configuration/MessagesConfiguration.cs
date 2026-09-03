using System.Collections.Concurrent;
using System.Reflection;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessagesConfiguration
{
    public required ConcurrentDictionary<Type, MessageConfiguration> MessagesByRuntimeType { get; set; }
    public required ConcurrentDictionary<string, MessageConfiguration> MessagesByKey { get; set; }
    public required Assembly[] Assemblies { get; set; }

    public static MessagesConfiguration Merge(MessagesConfiguration a, MessagesConfiguration b)
    {
        // Right now we are tossing away duplicates. However, it might be more interesting in the future
        // to merge some parts of the configurations, such as the routing keys.

        var messagesByRuntimeType = a.MessagesByRuntimeType.Concat(b.MessagesByRuntimeType).DistinctBy(x => x.Key);
        var messagesByKey = a.MessagesByKey.Concat(b.MessagesByKey).DistinctBy(x => x.Key);

        return new MessagesConfiguration
        {
            MessagesByRuntimeType = new ConcurrentDictionary<Type, MessageConfiguration>(messagesByRuntimeType),
            MessagesByKey = new ConcurrentDictionary<string, MessageConfiguration>(messagesByKey),
            Assemblies = a.Assemblies.Concat(b.Assemblies).Distinct().ToArray(),
        };
    }

    public void AddMessage(MessageConfiguration message)
    {
        MessagesByRuntimeType.TryAdd(message.RuntimeType, message);
    }
}