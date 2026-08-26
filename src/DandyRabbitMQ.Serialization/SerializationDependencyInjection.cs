using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Serialization;

public static class SerializationDependencyInjection
{
    public static IServiceCollection AddDandyRabbitMQSerialization(this IServiceCollection services, Action<SerializationConfigurationBuilder> builderAction)
    {
        if (services.BuildServiceProvider().GetService(typeof(IPayloadSerializer)) != null)
            return services;

        var builder = new SerializationConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        if (!configuration.IsInitialized())
            throw new InvalidOperationException("Payload serializer type is not set.");

        services.AddSingleton(typeof(IPayloadSerializer), configuration.PayloadSerializerType);
        if (configuration.PayloadSerializerConfiguration != null)
            services.AddSingleton(configuration.PayloadSerializerConfiguration);

        services.AddSingleton(configuration);

        return services;
    }
}