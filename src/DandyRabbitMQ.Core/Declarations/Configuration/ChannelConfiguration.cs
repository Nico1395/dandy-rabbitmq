using RabbitMQ.Client;

namespace DandyRabbitMQ.Core.Declarations.Configuration;

public sealed class ChannelConfiguration(string exchangeName, string queueName)
{
    public CreateChannelOptions? Options { get; set; }
    public ExchangeConfiguration Exchange { get; set; } = new(exchangeName);
    public QueueConfiguration Queue { get; set; } = new(queueName);
    public uint PrefetchSize { get; set; } = 0;
    public ushort PrefetchCount { get; set; } = 32;
    public bool Global { get; set; }
    public bool AutoAck { get; set; }
    public string? ConsumerTag { get; set; }
}