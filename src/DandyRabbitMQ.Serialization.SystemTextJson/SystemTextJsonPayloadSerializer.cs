using System.Text.Json;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

/// <summary>
/// Serializes payloads using <see cref="JsonSerializer"/>.
/// </summary>
public class SystemTextJsonPayloadSerializer(SystemTextJsonPayloadSerializerConfiguration configuration) : IPayloadSerializer
{
    /// <summary>
    /// Serializes a <paramref name="payload"/> using the configured System.Text.Json options.
    /// </summary>
    /// <param name="payload">The payload to serialize.</param>
    /// <param name="inputType">The type to use during serialization, or <see langword="null"/> to use the payload runtime type.</param>
    /// <returns>The serialized JSON <paramref name="payload"/>.</returns>
    public virtual string Serialize(object payload, Type? inputType)
    {
        inputType ??= payload.GetType();
        return JsonSerializer.Serialize(payload, inputType, configuration.JsonSerializerOptions);
    }

    /// <summary>
    /// Deserializes a JSON <paramref name="payload"/> using the configured System.Text.Json options.
    /// </summary>
    /// <param name="payload">The serialized JSON payload.</param>
    /// <param name="returnType">The type to which the payload is deserialized.</param>
    /// <returns>The deserialized <paramref name="payload"/>, or <see langword="null"/>.</returns>
    public virtual object? Deserialize(string payload, Type returnType)
    {
        return JsonSerializer.Deserialize(payload, returnType, configuration.JsonSerializerOptions);
    }
}