using System.Collections.Concurrent;
using System.Reflection;

namespace DandyRabbitMQ.Core.Messages.Metadata;

public static class MessageMetadataProvider
{
    private static readonly ConcurrentDictionary<Type, MessageMetadata> _metadata = [];

    public static MessageMetadata Get(Type messageType)
    {
        return _metadata.GetOrAdd(messageType, type =>
        {
            var attributes = type.GetCustomAttributes().ToArray();
            var exchange = GetExchange(attributes);
            var routingKeys = GetRoutingKeys(attributes);

            return new MessageMetadata
            {
                Exchange = exchange,
                RoutingKeys = routingKeys ?? Array.Empty<string>()
            };
        });
    }

    private static string? GetExchange(Attribute[] attributes)
    {
        var attribute = attributes.SingleOrDefault(a => a.GetType() == typeof(ExchangeAttribute)) as ExchangeAttribute;
        return attribute?.Exchange;
    }
    
    private static string[]? GetRoutingKeys(Attribute[] attributes)
    {
        var attribute = attributes.SingleOrDefault(a => a.GetType() == typeof(RoutingKeyAttribute)) as RoutingKeyAttribute;
        return attribute?.Keys;
    }
}