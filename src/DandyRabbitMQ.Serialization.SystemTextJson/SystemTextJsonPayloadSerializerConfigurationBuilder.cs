using System.Text.Json;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

public sealed class SystemTextJsonPayloadSerializerConfigurationBuilder
{
    private readonly SystemTextJsonPayloadSerializerConfiguration _configuration = new();

    public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();
    
    internal SystemTextJsonPayloadSerializerConfiguration Build()
    {
        _configuration.JsonSerializerOptions = JsonSerializerOptions;
        return _configuration;
    }
}