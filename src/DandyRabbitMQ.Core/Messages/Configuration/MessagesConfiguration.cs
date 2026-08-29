using System.Collections.Concurrent;
using System.Reflection;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessagesConfiguration
{
    public required ConcurrentDictionary<Type, MessageConfiguration> Messages { get; set; }
    public required Assembly[] Assemblies { get; set; }

    public static MessagesConfiguration Merge(MessagesConfiguration a, MessagesConfiguration b)
    {
        var merged = a.Messages.Concat(b.Messages).DistinctBy(x => x.Key);
        return new()
        {
            Messages = new ConcurrentDictionary<Type, MessageConfiguration>(merged),
            Assemblies = a.Assemblies.Concat(b.Assemblies).Distinct().ToArray(),
        };
    }
}