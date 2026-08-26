using System.Diagnostics.CodeAnalysis;

namespace DandyRabbitMQ.Serialization;

public sealed class SerializationConfiguration
{
    public Type? PayloadSerializerType { get; set; }
    public object? PayloadSerializerConfiguration { get; set; }

    [MemberNotNullWhen(true, nameof(PayloadSerializerType))]
    public bool IsInitialized()
    {
        return PayloadSerializerType != null;
    }
}