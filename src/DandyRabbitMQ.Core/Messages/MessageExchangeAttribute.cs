namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class MessageExchangeAttribute(string exchange) : MessageAttribute
{
    public string Exchange { get; } = exchange;
}