namespace DandyRabbitMQ.Serialization;

/// <summary>
/// Builds a <see cref="SerializationConfiguration"/>.
/// </summary>
public sealed class SerializationConfigurationBuilder
{
    private readonly SerializationConfiguration _configuration = new();

    /// <summary>
    /// Selects the payload serializer and its optional configuration.
    /// </summary>
    /// <param name="payloadSerializerType">The payload serializer type.</param>
    /// <param name="payloadSerializerConfiguration">The serializer configuration, or <see langword="null"/>.</param>
    /// <returns>This builder.</returns>
    public SerializationConfigurationBuilder UsePayloadSerializer(Type payloadSerializerType, object? payloadSerializerConfiguration)
    {
        _configuration.PayloadSerializerType = payloadSerializerType;
        _configuration.PayloadSerializerConfiguration = payloadSerializerConfiguration;

        return this;
    }

    /// <summary>
    /// Builds the configured serialization settings.
    /// </summary>
    /// <returns>The serialization configuration.</returns>
    public SerializationConfiguration Build() => _configuration;
}