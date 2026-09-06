using Newtonsoft.Json;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

/// <summary>
/// Provides predefined Newtonsoft.Json settings.
/// </summary>
public static class JsonSerializerSettingsPresets
{
    /// <summary>
    /// Creates the default Newtonsoft.Json settings.
    /// </summary>
    /// <returns>New default serializer settings.</returns>
    public static JsonSerializerSettings Default()
    {
        return new JsonSerializerSettings();
    }

    /// <summary>
    /// Creates settings that preserve and consume .NET type names.
    /// </summary>
    /// <returns>Serializer settings with <see cref="TypeNameHandling.All"/> enabled.</returns>
    public static JsonSerializerSettings WithTypeNameHandling()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
    }
}
