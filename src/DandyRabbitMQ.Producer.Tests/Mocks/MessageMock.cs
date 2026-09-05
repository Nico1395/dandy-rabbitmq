using DandyRabbitMQ.Core.Messages;

namespace DandyRabbitMQ.Producer.Tests.Mocks;

internal sealed class MessageMock : Message
{
    public required string Content { get; init; }

    public static MessageMock New()
    {
        return new MessageMock { Content = "Hello world!" };
    }

    public static MessageMock New(string content)
    {
        return new MessageMock { Content = content };
    }
}