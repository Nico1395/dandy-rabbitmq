using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

public sealed class NewtonsoftJsonPayloadSerializerConfiguration
{
    public JsonSerializerSettings JsonSerializerSettings { get; set; } = JsonSerializerSettingsPresets.SharedMessageTypes();
}