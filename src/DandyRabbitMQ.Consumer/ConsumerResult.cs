namespace DandyRabbitMQ.Consumer;

public sealed class ConsumerResult
{
    private ConsumerResult() { }

    public required ConsumerStatus Status { get; init; }
    public bool Requeue { get; init; }
    public bool Multiple { get; init; }

    public static ConsumerResult Ack()
    {
        return new ConsumerResult
        {
            Status = ConsumerStatus.Ack,
        };
    }

    public static ConsumerResult AckMultiple()
    {
        return new ConsumerResult
        {
            Status = ConsumerStatus.Ack,
            Multiple = true,
        };
    }

    public static ConsumerResult Nack()
    {
        return new ConsumerResult
        {
            Status = ConsumerStatus.Nack,
        };
    }

    public static ConsumerResult NackRequeue()
    {
        return new ConsumerResult
        {
            Status = ConsumerStatus.Nack,
            Requeue = true,
        };
    }

    public static ConsumerResult NackMultiple()
    {
        return new ConsumerResult
        {
            Status = ConsumerStatus.Ack,
            Multiple = true,
        };
    }

    public static ConsumerResult NackRequeueMultiple()
    {
        return new ConsumerResult
        {
            Status = ConsumerStatus.Nack,
            Requeue = true,
            Multiple = true,
        };
    }
}