using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

/// <summary>
/// Configures the Newtonsoft.Json payload serializer.
/// </summary>
public sealed class NewtonsoftJsonPayloadSerializerConfiguration
{
    /// <summary>
    /// Gets or sets the Newtonsoft.Json settings.
    /// </summary>
    public JsonSerializerSettings JsonSerializerSettings { get; set; } = JsonSerializerSettingsPresets.Default();
}