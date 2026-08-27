namespace DandyRabbitMQ.Consumer.Configuration;

public sealed class QueueConfiguration
{
    public string? Name { get; set; }
    public string[]? RoutingKeys { get; set; }
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public bool NoWait { get; set; }
    public Dictionary<string, object?> Arguments { get; set; } = new()
    {
        { "x-queue-type", "quorum" },
    };
}