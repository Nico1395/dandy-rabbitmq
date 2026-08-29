namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class RoutingKeyAttribute(params string[] keys) : MessageAttribute
{
    public string[] Keys { get; } = keys;
}