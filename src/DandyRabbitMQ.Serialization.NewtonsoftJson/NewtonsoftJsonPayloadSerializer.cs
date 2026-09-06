using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

/// <summary>
/// Serializes payloads using Newtonsoft.Json.
/// </summary>
public class NewtonsoftJsonPayloadSerializer(NewtonsoftJsonPayloadSerializerConfiguration configuration) : IPayloadSerializer
{
    /// <summary>
    /// Serializes a payload using the configured Newtonsoft.Json settings.
    /// </summary>
    /// <param name="payload">The payload to serialize.</param>
    /// <param name="inputType">The type to use during serialization.</param>
    /// <returns>The serialized JSON payload.</returns>
    public string Serialize(object payload, Type? inputType)
    {
        return JsonConvert.SerializeObject(payload, inputType, configuration.JsonSerializerSettings);
    }

    /// <summary>
    /// Deserializes a JSON payload using the configured Newtonsoft.Json settings.
    /// </summary>
    /// <param name="payload">The serialized JSON payload.</param>
    /// <param name="returnType">The type to which the payload is deserialized.</param>
    /// <returns>The deserialized payload, or <see langword="null"/>.</returns>
    public object? Deserialize(string payload, Type returnType)
    {
        return JsonConvert.DeserializeObject(payload, returnType, configuration.JsonSerializerSettings);
    }
}