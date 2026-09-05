using DandyRabbitMQ.Consumer;
using DandyRabbitMQ.Sample.Shared;

namespace DandyRabbitMQ.Sample.Consumer;

internal sealed class InputMessageV1Consumer : IConsumer<InputMessageV1>
{
    public Task<ConsumerResult> ConsumeAsync(InputMessageV1 message, ConsumerContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Message received: {message.Text}");
        return Task.FromResult(ConsumerResult.Ack());
    }
}