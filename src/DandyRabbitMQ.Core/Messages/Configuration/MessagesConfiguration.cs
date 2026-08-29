using System.Collections.Concurrent;
using System.Reflection;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessagesConfiguration
{
    public required ConcurrentDictionary<Type, MessageConfiguration> Messages { get; set; }
    public required Assembly[] Assemblies { get; set; }
}