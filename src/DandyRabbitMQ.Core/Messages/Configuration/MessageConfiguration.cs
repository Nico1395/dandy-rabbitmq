namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessageConfiguration
{
    public required Type Type { get; init; }
    public string? Exchange { get; set; }
    public string[]? RoutingKeys { get; set; }
    public bool RouteExclusiveSerialization { get; set; }
}