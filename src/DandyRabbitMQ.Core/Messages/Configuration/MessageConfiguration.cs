namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessageConfiguration
{
    public required Type RuntimeType { get; init; }
    public string? Type { get; set; }
    public string? Exchange { get; set; }
    public string[]? RoutingKeys { get; set; }
    public required IReadOnlyDictionary<string, object> Metadata { get; init; }
}