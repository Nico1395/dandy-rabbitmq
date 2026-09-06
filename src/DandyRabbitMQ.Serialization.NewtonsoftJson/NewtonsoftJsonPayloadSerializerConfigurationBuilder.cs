using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

/// <summary>
/// Builds a <see cref="NewtonsoftJsonPayloadSerializerConfiguration"/>.
/// </summary>
public sealed class NewtonsoftJsonPayloadSerializerConfigurationBuilder
{
    private readonly NewtonsoftJsonPayloadSerializerConfiguration _configuration = new();
    /// <summary>
    /// Gets or sets the Newtonsoft.Json settings.
    /// </summary>
    public JsonSerializerSettings JsonSerializerSettings { get; set; } = new();

    internal NewtonsoftJsonPayloadSerializerConfiguration Build()
    {
        _configuration.JsonSerializerSettings = JsonSerializerSettings;
        return _configuration;
    }
}
