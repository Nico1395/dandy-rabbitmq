using DandyRabbitMQ.Core.Encoding.Encodings;

namespace DandyRabbitMQ.Core.Encoding.Configuration;

public sealed class EncodingConfiguration
{
    public Type EncoderImplementationType { get; set; } = typeof(Utf8PayloadEncoder);
}