namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MessageMetadataAttribute(string key, object value) : MessageAttribute
{
    public string Key { get; } = key;
    public object Value { get; } = value;
}