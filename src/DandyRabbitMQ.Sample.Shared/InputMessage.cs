using DandyRabbitMQ.Core.Messages;

namespace DandyRabbitMQ.Sample.Shared;

public sealed class InputMessage : Message
{
    public required string Text { get; init; }
}