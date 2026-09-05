namespace DandyRabbitMQ.Core.Encoding.Encodings;

public class UnicodePayloadEncoder : IPayloadEncoder
{
    public ReadOnlyMemory<byte> Encode(string payload)
    {
        return System.Text.Encoding.Unicode.GetBytes(payload);
    }

    public string Decode(ReadOnlySpan<byte> bytes)
    {
        return System.Text.Encoding.Unicode.GetString(bytes);
    }
}