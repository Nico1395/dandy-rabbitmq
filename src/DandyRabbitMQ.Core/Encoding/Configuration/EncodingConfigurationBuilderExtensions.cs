using DandyRabbitMQ.Core.Encoding.Encodings;

namespace DandyRabbitMQ.Core.Encoding.Configuration;

public static class EncodingConfigurationBuilderExtensions
{
    public static EncodingConfigurationBuilder UseAsciiPayloadEncoder(this EncodingConfigurationBuilder builder)
    {
        return builder.UseEncoder(typeof(AsciiPayloadEncoder));
    }

    public static EncodingConfigurationBuilder UseBigEndianUnicodePayloadEncoder(this EncodingConfigurationBuilder builder)
    {
        return builder.UseEncoder(typeof(BigEndianUnicodePayloadEncoder));
    }

    public static EncodingConfigurationBuilder UseLatin1PayloadEncoder(this EncodingConfigurationBuilder builder)
    {
        return builder.UseEncoder(typeof(Latin1PayloadEncoder));
    }

    public static EncodingConfigurationBuilder UseUnicodePayloadEncoder(this EncodingConfigurationBuilder builder)
    {
        return builder.UseEncoder(typeof(UnicodePayloadEncoder));
    }

    public static EncodingConfigurationBuilder UseUtf8PayloadEncoder(this EncodingConfigurationBuilder builder)
    {
        return builder.UseEncoder(typeof(Utf8PayloadEncoder));
    }

    public static EncodingConfigurationBuilder UseUtf32PayloadEncoder(this EncodingConfigurationBuilder builder)
    {
        return builder.UseEncoder(typeof(Utf32PayloadEncoder));
    }
}