using System.Collections.Concurrent;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessageConfigurationBuilder
{
    private readonly ConcurrentDictionary<string, object> _metadata;
    private readonly MessageConfiguration _message;

    public MessageConfigurationBuilder(Type messageType)
    {
        _metadata = [];
        _message = new MessageConfiguration
        {
            RuntimeType = messageType,
            Metadata = _metadata,
        };
    }

    public MessageConfigurationBuilder SetType(string type)
    {
        _message.Type = type;
        return this;
    }

    public MessageConfigurationBuilder SetExchange(string exchange)
    {
        _message.Exchange = exchange;
        return this;
    }
    
    public MessageConfigurationBuilder SetRoutingKeys(params string[] routingKeys)
    {
        _message.RoutingKeys = routingKeys;
        return this;
    }

    public MessageConfigurationBuilder AddMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }

    public MessageConfiguration Build()
    {
        return _message;
    }
}