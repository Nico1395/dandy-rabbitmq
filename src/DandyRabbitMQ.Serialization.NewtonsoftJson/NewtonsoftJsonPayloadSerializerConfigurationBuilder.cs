using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

public sealed class NewtonsoftJsonPayloadSerializerConfigurationBuilder
{
    private readonly NewtonsoftJsonPayloadSerializerConfiguration _configuration = new();
    
    public JsonSerializerSettings JsonSerializerSettings { get; set; } = new();

    internal NewtonsoftJsonPayloadSerializerConfiguration Build()
    {
        _configuration.JsonSerializerSettings = JsonSerializerSettings;
        return _configuration;   
    }
}