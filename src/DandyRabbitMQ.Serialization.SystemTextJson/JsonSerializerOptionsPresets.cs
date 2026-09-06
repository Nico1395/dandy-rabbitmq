using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace DandyRabbitMQ.Serialization.SystemTextJson;

/// <summary>
/// Provides predefined System.Text.Json options.
/// </summary>
public static class JsonSerializerOptionsPresets
{
    /// <summary>
    /// Creates the default System.Text.Json options.
    /// </summary>
    /// <returns>New default serializer options.</returns>
    public static JsonSerializerOptions Default()
    {
        return new JsonSerializerOptions();
    }

    /// <summary>
    /// Creates options configured with polymorphic type information.
    /// </summary>
    /// <returns>Serializer options using the resolver returned by <see cref="JsonSerializerOptionsPresets.CreateTypeInfoResolver()"/>.</returns>
    public static JsonSerializerOptions WithTypeInfoResolver()
    {
        return new JsonSerializerOptions
        {
            TypeInfoResolver = CreateTypeInfoResolver(),
        };
    }

    /// <summary>
    /// Creates a resolver that enables fallback polymorphic serialization for object types.
    /// </summary>
    /// <returns>A configured JSON type information resolver.</returns>
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
