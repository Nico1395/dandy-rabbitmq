using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Core.Encoding.Configuration;

public static class EncodingServiceCollectionExtensions
{
    public static IServiceCollection AddDandyRabbitMQEncoding(this IServiceCollection services, Action<EncodingConfigurationBuilder> builderAction)
    {
        var builder = new EncodingConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        return services.AddDandyRabbitMQEncoding(configuration);
    }

    public static IServiceCollection AddDandyRabbitMQEncoding(this IServiceCollection services, EncodingConfiguration configuration)
    {
        if (services.BuildServiceProvider().GetService(typeof(IPayloadEncoder)) != null)
            return services;

        if (configuration.EncoderImplementationType.IsAbstract)
            throw new InvalidOperationException("Encoder implementation type is abstract.");

        var payloadEncoderInterface = typeof(IPayloadEncoder);
        if (!configuration.EncoderImplementationType.IsAssignableTo(payloadEncoderInterface))
            throw new InvalidOperationException($"Encoder implementation type does not implement {payloadEncoderInterface}.");

        return services.AddSingleton(payloadEncoderInterface, configuration.EncoderImplementationType);
    }
}