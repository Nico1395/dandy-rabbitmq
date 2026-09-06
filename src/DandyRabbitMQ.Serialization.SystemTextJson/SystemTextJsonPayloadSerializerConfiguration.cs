using System.Text.Json;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

/// <summary>
/// Configures the System.Text.Json payload serializer.
/// </summary>
public sealed class SystemTextJsonPayloadSerializerConfiguration
{
    /// <summary>
    /// Gets or sets the System.Text.Json options.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = JsonSerializerOptionsPresets.Default();
}
