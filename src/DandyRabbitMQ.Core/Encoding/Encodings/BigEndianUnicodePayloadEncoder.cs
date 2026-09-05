namespace DandyRabbitMQ.Core.Encoding.Encodings;

public class BigEndianUnicodePayloadEncoder : IPayloadEncoder
{
    public ReadOnlyMemory<byte> Encode(string payload)
    {
        return System.Text.Encoding.BigEndianUnicode.GetBytes(payload);
    }

    public string Decode(ReadOnlySpan<byte> bytes)
    {
        return System.Text.Encoding.BigEndianUnicode.GetString(bytes);
    }
}