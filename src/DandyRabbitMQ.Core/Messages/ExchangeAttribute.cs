namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class ExchangeAttribute(string exchange) : MessageAttribute
{
    public string Exchange { get; } = exchange;
}