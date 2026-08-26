namespace DandyRabbitMQ.Core.Messages;

public interface IMessage
{
    Guid Id { get; }
    DateTime Timestamp { get; }
}