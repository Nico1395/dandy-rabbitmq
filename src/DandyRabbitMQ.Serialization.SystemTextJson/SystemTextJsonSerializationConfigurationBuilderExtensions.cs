namespace DandyRabbitMQ.Serialization.SystemTextJson;

public static class SystemTextJsonSerializationConfigurationBuilderExtensions
{
    public static SerializationConfigurationBuilder UseSystemTextJson(this SerializationConfigurationBuilder serializationConfigurationBuilder, Action<SystemTextJsonPayloadSerializerConfigurationBuilder>? builderAction = null)
    {
        var builder = new SystemTextJsonPayloadSerializerConfigurationBuilder();
        builderAction?.Invoke(builder);

        return serializationConfigurationBuilder.UsePayloadSerializer(
            typeof(SystemTextJsonPayloadSerializer),
            builder.Build());
    }
}