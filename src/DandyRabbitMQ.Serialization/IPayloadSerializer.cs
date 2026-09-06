namespace DandyRabbitMQ.Serialization;

/// <summary>
/// Serializes message payloads to and from their transport representation.
/// </summary>
public interface IPayloadSerializer
{
    /// <summary>
    /// Serializes a <paramref name="payload"/>.
    /// </summary>
    /// <param name="payload">The payload to serialize.</param>
    /// <param name="inputType">The runtime type to use, or <see langword="null"/> to let the implementation determine it.</param>
    /// <returns>The serialized <paramref name="payload"/>.</returns>
    string Serialize(object payload, Type? inputType);

    /// <summary>
    /// Deserializes a <paramref name="payload"/> to the specified type.
    /// </summary>
    /// <param name="payload">The serialized payload.</param>
    /// <param name="returnType">The type to which the payload is deserialized.</param>
    /// <returns>The deserialized <paramref name="payload"/>, or <see langword="null"/> when no value is produced.</returns>
    object? Deserialize(string payload, Type returnType);
}