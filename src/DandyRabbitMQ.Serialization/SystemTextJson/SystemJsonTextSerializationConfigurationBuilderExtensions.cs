namespace DandyRabbitMQ.Serialization.SystemTextJson;

public static class SystemJsonTextSerializationConfigurationBuilderExtensions
{
    public static SerializationConfigurationBuilder UseSystemTextJson(this SerializationConfigurationBuilder serializationConfigurationBuilder, Action<SystemTextJsonPayloadSerializerConfigurationBuilder> builderAction)
    {
        var builder = new SystemTextJsonPayloadSerializerConfigurationBuilder();
        builderAction.Invoke(builder);

        return serializationConfigurationBuilder.UsePayloadSerializer(
            typeof(SystemTextJsonPayloadSerializer),
            builder.Build());
    }
}