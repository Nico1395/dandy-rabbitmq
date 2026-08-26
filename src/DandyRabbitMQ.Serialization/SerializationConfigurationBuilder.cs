namespace DandyRabbitMQ.Serialization;

public sealed class SerializationConfigurationBuilder
{
    private readonly SerializationConfiguration _configuration = new();

    public SerializationConfigurationBuilder UsePayloadSerializer(Type payloadSerializerType, object? payloadSerializerConfiguration)
    {
        _configuration.PayloadSerializerType = payloadSerializerType;
        _configuration.PayloadSerializerConfiguration = payloadSerializerConfiguration;

        return this;
    }
    
    internal SerializationConfiguration Build() => _configuration;
}