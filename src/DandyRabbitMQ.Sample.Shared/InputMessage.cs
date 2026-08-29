using DandyRabbitMQ.Core.Messages;

namespace DandyRabbitMQ.Sample.Shared;

[DistinctMessage]
[Exchange("messages")]
[RoutingKey("all")]
public sealed class InputMessage : Message
{
    public required string Text { get; init; }
}