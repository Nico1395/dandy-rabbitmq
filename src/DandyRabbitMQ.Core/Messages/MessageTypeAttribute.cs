namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class MessageTypeAttribute(string type) : MessageAttribute
{
    public string Type { get; } = type;
}