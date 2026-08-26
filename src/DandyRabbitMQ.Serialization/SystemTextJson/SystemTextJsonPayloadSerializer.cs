using System.Text.Json;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

public class SystemTextJsonPayloadSerializer(SystemTextJsonPayloadSerializerConfiguration configuration) : IPayloadSerializer
{
    public virtual string Serialize(object payload, Type? inputType)
    {
        inputType ??= payload.GetType();
        return JsonSerializer.Serialize(payload, inputType, configuration.JsonSerializerOptions);
    }

    public virtual object? Deserialize(string payload, Type returnType)
    {
        return JsonSerializer.Deserialize(payload, returnType, configuration.JsonSerializerOptions);
    }
}