namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class MessageKeyAttribute(string key) : MessageAttribute
{
    public string Key { get; } = key;
}