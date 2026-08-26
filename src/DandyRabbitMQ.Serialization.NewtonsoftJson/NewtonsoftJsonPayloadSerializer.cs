using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

public class NewtonsoftJsonPayloadSerializer(NewtonsoftJsonPayloadSerializerConfiguration configuration) : IPayloadSerializer
{
    public string Serialize(object payload, Type? inputType)
    {
        return JsonConvert.SerializeObject(inputType, inputType, configuration.JsonSerializerSettings);
    }

    public object? Deserialize(string payload, Type returnType)
    {
        return JsonConvert.DeserializeObject(payload, returnType, configuration.JsonSerializerSettings);
    }
}