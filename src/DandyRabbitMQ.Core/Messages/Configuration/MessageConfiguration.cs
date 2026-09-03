namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessageConfiguration
{
    public required Type RuntimeType { get; init; }
    public string? Type { get; set; }       // TODO -> Remember to always default this to the type name
    public string? Exchange { get; set; }
    public string[]? RoutingKeys { get; set; }
    public required IReadOnlyDictionary<string, object> Metadata { get; init; }
}