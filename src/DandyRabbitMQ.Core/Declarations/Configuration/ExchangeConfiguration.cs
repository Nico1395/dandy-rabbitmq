namespace DandyRabbitMQ.Core.Declarations.Configuration;

public sealed class ExchangeConfiguration(string name)
{
    public string Name { get; } = name;
    public string ExchangeType { get; set; } = RabbitMQ.Client.ExchangeType.Topic;
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
}