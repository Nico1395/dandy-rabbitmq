using System.Diagnostics.CodeAnalysis;

namespace DandyRabbitMQ.Serialization;

/// <summary>
/// Configures the payload serializer used by DandyRabbitMQ.
/// </summary>
public sealed class SerializationConfiguration
{
    /// <summary>
    /// Gets or sets the type registered as the payload serializer.
    /// </summary>
    public Type? PayloadSerializerType { get; set; }

    /// <summary>
    /// Gets or sets the configuration instance supplied to the payload serializer.
    /// </summary>
    public object? PayloadSerializerConfiguration { get; set; }

    /// <summary>
    /// Determines whether a payload serializer type has been configured.
    /// </summary>
    /// <returns><see langword="true"/> when <see cref="PayloadSerializerType"/> is set; otherwise, <see langword="false"/>.</returns>
    [MemberNotNullWhen(true, nameof(PayloadSerializerType))]
    public bool IsInitialized()
    {
        return PayloadSerializerType != null;
    }
}