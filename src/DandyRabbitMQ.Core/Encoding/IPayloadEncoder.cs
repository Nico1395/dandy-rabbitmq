namespace DandyRabbitMQ.Core.Encoding;

public interface IPayloadEncoder
{
    ReadOnlyMemory<byte> Encode(string payload);
    string Decode(ReadOnlySpan<byte> bytes);
}