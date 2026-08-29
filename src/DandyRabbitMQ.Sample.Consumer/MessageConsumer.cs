using DandyRabbitMQ.Consumer;
using DandyRabbitMQ.Sample.Shared;

namespace DandyRabbitMQ.Sample.Consumer;

internal sealed class MessageConsumer : IConsumer<InputMessage>
{
    public Task<ConsumerResult> ConsumeAsync(InputMessage message, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Message received: {message.Text}");
        return Task.FromResult(ConsumerResult.Ack());
    }
}