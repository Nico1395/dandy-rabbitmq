namespace DandyRabbitMQ.Core.Encoding.Encodings;

public class Latin1PayloadEncoder : IPayloadEncoder
{
    public ReadOnlyMemory<byte> Encode(string payload)
    {
        return System.Text.Encoding.Latin1.GetBytes(payload);
    }

    public string Decode(ReadOnlySpan<byte> bytes)
    {
        return System.Text.Encoding.Latin1.GetString(bytes);
    }
}