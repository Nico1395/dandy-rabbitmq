namespace DandyRabbitMQ.Core.Messages;

[AttributeUsage(AttributeTargets.Class)]
public class ExchangeAttribute(string exchange) : Attribute
{
    public string Exchange { get; } = exchange;
}