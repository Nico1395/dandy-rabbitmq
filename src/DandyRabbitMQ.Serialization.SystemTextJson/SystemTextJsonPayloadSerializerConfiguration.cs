using System.Text.Json;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

public sealed class SystemTextJsonPayloadSerializerConfiguration
{
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = JsonSerializerOptionsPresets.SharedMessageTypes();
}