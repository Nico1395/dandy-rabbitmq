namespace DandyRabbitMQ.Serialization;

/// <summary>
/// Provides convenience methods for <see cref="IPayloadSerializer"/> implementations.
/// </summary>
public static class PayloadSerializerExtensions
{
    /// <summary>
    /// Serializes a payload using its runtime type.
    /// </summary>
    /// <param name="serializer">The serializer to use.</param>
    /// <param name="payload">The payload to serialize.</param>
    /// <returns>The serialized payload.</returns>
    public static string Serialize(this IPayloadSerializer serializer, object payload)
    {
        return serializer.Serialize(payload, payload.GetType());
    }

    /// <summary>
    /// Deserializes a payload to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to which the payload is deserialized.</typeparam>
    /// <param name="serializer">The serializer to use.</param>
    /// <param name="payload">The serialized payload.</param>
    /// <returns>The deserialized payload, or the default value when the result is not of type <typeparamref name="T"/>.</returns>
    public static T? Deserialize<T>(this IPayloadSerializer serializer, string payload)
    {
        return serializer.Deserialize(payload, typeof(T)) is T casted ? casted : default;
    }
}