using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

public static class JsonSerializerSettingsPresets
{
    public static JsonSerializerSettings Default()
    {
        return new JsonSerializerSettings();
    }

    public static JsonSerializerSettings WithTypeNameHandling()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
    }
}