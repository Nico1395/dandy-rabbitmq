namespace DandyRabbitMQ.Core.Encoding.Encodings;

public class Utf8PayloadEncoder : IPayloadEncoder
{
    public ReadOnlyMemory<byte> Encode(string payload)
    {
        return System.Text.Encoding.UTF8.GetBytes(payload);
    }

    public string Decode(ReadOnlySpan<byte> bytes)
    {
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}