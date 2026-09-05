namespace DandyRabbitMQ.Core.Encoding.Encodings;

public class Utf32PayloadEncoder : IPayloadEncoder
{
    public ReadOnlyMemory<byte> Encode(string payload)
    {
        return System.Text.Encoding.UTF32.GetBytes(payload);
    }

    public string Decode(ReadOnlySpan<byte> bytes)
    {
        return System.Text.Encoding.UTF32.GetString(bytes);
    }
}