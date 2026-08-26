namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class RoutingKeyAttribute(params string[] keys) : Attribute
{
    public string[] Keys { get; } = keys;
}