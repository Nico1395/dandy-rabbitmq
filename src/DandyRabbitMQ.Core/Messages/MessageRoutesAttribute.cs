namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class MessageRoutesAttribute(params string[] keys) : MessageAttribute
{
    public string[] Keys { get; } = keys;
}