namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

/// <summary>
/// Provides Newtonsoft.Json integration for the serialization configuration builder.
/// </summary>
public static class NewtonsoftJsonSerializationConfigurationBuilderExtensions
{
    /// <summary>
    /// Configures Newtonsoft.Json as the payload serializer.
    /// </summary>
    /// <param name="serializationConfigurationBuilder">The serialization configuration builder.</param>
    /// <param name="builderAction">An optional action that configures Newtonsoft.Json.</param>
    /// <returns>The updated serialization configuration builder.</returns>
    public static SerializationConfigurationBuilder UseNewtonsoftJson(this SerializationConfigurationBuilder serializationConfigurationBuilder, Action<NewtonsoftJsonPayloadSerializerConfigurationBuilder>? builderAction = null)
    {
        var builder = new NewtonsoftJsonPayloadSerializerConfigurationBuilder();
        builderAction?.Invoke(builder);

        return serializationConfigurationBuilder.UsePayloadSerializer(
            typeof(NewtonsoftJsonPayloadSerializer),
            builder.Build());
    }
}