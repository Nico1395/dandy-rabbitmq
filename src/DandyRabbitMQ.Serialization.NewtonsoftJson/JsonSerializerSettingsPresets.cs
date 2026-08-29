using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

public static class JsonSerializerSettingsPresets
{
    public static JsonSerializerSettings Default()
    {
        return new JsonSerializerSettings();
    }

    public static JsonSerializerSettings DistinctMessageTypes()
    {
        return new JsonSerializerSettings();
    }

    public static JsonSerializerSettings SharedMessageTypes()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
    }
}