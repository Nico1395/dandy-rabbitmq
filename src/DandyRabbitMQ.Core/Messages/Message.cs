namespace DandyRabbitMQ.Core.Messages;

public abstract class Message : IMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}