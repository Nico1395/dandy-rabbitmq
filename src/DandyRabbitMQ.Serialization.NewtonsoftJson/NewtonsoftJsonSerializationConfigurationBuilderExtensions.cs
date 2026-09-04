namespace DandyRabbitMQ.Serialization.NewtonsoftJson;

public static class NewtonsoftJsonSerializationConfigurationBuilderExtensions
{
    public static SerializationConfigurationBuilder UseNewtonsoftJson(this SerializationConfigurationBuilder serializationConfigurationBuilder, Action<NewtonsoftJsonPayloadSerializerConfigurationBuilder>? builderAction = null)
    {
        var builder = new NewtonsoftJsonPayloadSerializerConfigurationBuilder();
        builderAction?.Invoke(builder);

        return serializationConfigurationBuilder.UsePayloadSerializer(
            typeof(NewtonsoftJsonPayloadSerializer),
            builder.Build());
    }
}