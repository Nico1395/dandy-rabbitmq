namespace DandyRabbitMQ.Core.Encoding;

/// <summary>Encodes and decodes serialized payloads.</summary>
public interface IPayloadEncoder
{
    /// <summary>Encodes a string payload as bytes.</summary>
    /// <param name="payload">The payload to encode.</param>
    /// <returns>The encoded payload.</returns>
    ReadOnlyMemory<byte> Encode(string payload);

    /// <summary>Decodes bytes into a string payload.</summary>
    /// <param name="bytes">The bytes to decode.</param>
    /// <returns>The decoded payload.</returns>
    string Decode(ReadOnlySpan<byte> bytes);
}