using RabbitMQ.Client;

namespace DandyRabbitMQ.Consumer.Configuration;

public sealed class ChannelConfiguration
{
    public CreateChannelOptions? Options { get; set; }
    public ExchangeConfiguration Exchange { get; set; } = new();
    public QueueConfiguration Queue { get; set; } = new();
    public uint PrefetchSize { get; set; } = 0;
    public ushort PrefetchCount { get; set; } = 32;
    public bool Global { get; set; }
    public bool AutoAck { get; set; }
    public string? ConsumerTag { get; set; }
}