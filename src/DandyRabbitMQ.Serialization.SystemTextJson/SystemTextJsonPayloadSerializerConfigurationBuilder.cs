using System.Text.Json;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

/// <summary>
/// Builds a <see cref="SystemTextJsonPayloadSerializerConfiguration"/>.
/// </summary>
public sealed class SystemTextJsonPayloadSerializerConfigurationBuilder
{
    private readonly SystemTextJsonPayloadSerializerConfiguration _configuration = new();
    /// <summary>
    /// Gets or sets the System.Text.Json options.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();

    internal SystemTextJsonPayloadSerializerConfiguration Build()
    {
        _configuration.JsonSerializerOptions = JsonSerializerOptions;
        return _configuration;
    }
}
