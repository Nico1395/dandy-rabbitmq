namespace DandyRabbitMQ.Core.Encoding.Encodings;

public class AsciiPayloadEncoder : IPayloadEncoder
{
    public ReadOnlyMemory<byte> Encode(string payload)
    {
        return System.Text.Encoding.ASCII.GetBytes(payload);
    }

    public string Decode(ReadOnlySpan<byte> bytes)
    {
        return System.Text.Encoding.ASCII.GetString(bytes);
    }
}