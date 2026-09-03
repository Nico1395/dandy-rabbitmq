using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

public static class JsonSerializerOptionsPresets
{
    public static JsonSerializerOptions Default()
    {
        return new JsonSerializerOptions();
    }

    public static JsonSerializerOptions WithTypeInfoResolver()
    {
        return new JsonSerializerOptions
        {
            TypeInfoResolver = CreateTypeInfoResolver(),
        };
    }

    public static IJsonTypeInfoResolver CreateTypeInfoResolver()
    {
        return new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                static typeInfo =>
                {
                    if (typeInfo.Kind == JsonTypeInfoKind.Object)
                    {
                        typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "$type",
                            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType,
                        };
                    }
                }
            }
        };
    }
}