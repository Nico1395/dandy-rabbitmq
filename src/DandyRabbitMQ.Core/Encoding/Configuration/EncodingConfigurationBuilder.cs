namespace DandyRabbitMQ.Core.Encoding.Configuration;

public sealed class EncodingConfigurationBuilder
{
    private readonly EncodingConfiguration _configuration = new();

    public EncodingConfigurationBuilder UseEncoder(Type implementationType)
    {
        _configuration.EncoderImplementationType = implementationType;
        return this;
    }

    public EncodingConfiguration Build()
    {
        return _configuration;
    }
}