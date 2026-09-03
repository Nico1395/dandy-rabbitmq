using System.Collections.Concurrent;
using System.Reflection;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessagesConfiguration
{
    public required ConcurrentDictionary<Type, MessageConfiguration> MessagesByRuntimeType { get; set; }
    public required Assembly[] Assemblies { get; set; }

    public static MessagesConfiguration Merge(MessagesConfiguration a, MessagesConfiguration b)
    {
        var merged = a.MessagesByRuntimeType.Concat(b.MessagesByRuntimeType).DistinctBy(x => x.Key);
        return new()
        {
            MessagesByRuntimeType = new ConcurrentDictionary<Type, MessageConfiguration>(merged),
            Assemblies = a.Assemblies.Concat(b.Assemblies).Distinct().ToArray(),
        };
    }

    public void AddMessage(MessageConfiguration message)
    {
        MessagesByRuntimeType.TryAdd(message.RuntimeType, message);
    }
}