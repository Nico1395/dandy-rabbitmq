namespace DandyRabbitMQ.Core.Messages.Metadata;

public sealed class MessageMetadata
{
    public string? Exchange { get; init; }
    public required string[] RoutingKeys { get; init; }
}