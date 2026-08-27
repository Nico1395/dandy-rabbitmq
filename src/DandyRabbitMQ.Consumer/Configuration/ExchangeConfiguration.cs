namespace DandyRabbitMQ.Consumer.Configuration;

public sealed class ExchangeConfiguration
{
    public string? Name { get; set; }
    public string ExchangeType { get; set; } = RabbitMQ.Client.ExchangeType.Topic;
    public bool Durable { get; set; } = true;
    public bool AutoDelete { get; set; } = false;
}