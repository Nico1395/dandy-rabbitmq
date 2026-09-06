namespace DandyRabbitMQ.Serialization.SystemTextJson;

/// <summary>
/// Provides System.Text.Json integration for the serialization configuration builder.
/// </summary>
public static class SystemTextJsonSerializationConfigurationBuilderExtensions
{
    /// <summary>
    /// Configures System.Text.Json as the payload serializer.
    /// </summary>
    /// <param name="serializationConfigurationBuilder">The serialization configuration builder.</param>
    /// <param name="builderAction">An optional action that configures System.Text.Json.</param>
    /// <returns>The updated serialization configuration builder.</returns>
    public static SerializationConfigurationBuilder UseSystemTextJson(this SerializationConfigurationBuilder serializationConfigurationBuilder, Action<SystemTextJsonPayloadSerializerConfigurationBuilder>? builderAction = null)
    {
        var builder = new SystemTextJsonPayloadSerializerConfigurationBuilder();
        builderAction?.Invoke(builder);

        return serializationConfigurationBuilder.UsePayloadSerializer(
            typeof(SystemTextJsonPayloadSerializer),
            builder.Build());
    }
}