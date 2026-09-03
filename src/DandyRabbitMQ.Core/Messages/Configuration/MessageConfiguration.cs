namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessageConfiguration
{
    public required Type RuntimeType { get; init; }
    public required string Key { get; set; }
    public string? Exchange { get; set; }
    public string[]? RoutingKeys { get; set; }
    public required IReadOnlyDictionary<string, object> Metadata { get; init; }
}