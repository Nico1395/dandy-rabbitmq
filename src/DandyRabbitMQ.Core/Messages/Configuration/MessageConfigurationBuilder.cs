namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessageConfigurationBuilder(Type messageType)
{
    private readonly MessageConfiguration _message = new()
    {
        Type = messageType,
    };

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

    public MessageConfigurationBuilder IsDistinctMessageType()
    {
        _message.RouteExclusiveSerialization = true;
        return this;
    }

    public MessageConfiguration Build()
    {
        return _message;
    }
}