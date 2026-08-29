using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Serialization;

public static class SerializationDependencyInjection
{
    public static IServiceCollection AddDandyRabbitMQSerialization(this IServiceCollection services, Action<SerializationConfigurationBuilder> builderAction)
    {
        var builder = new SerializationConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        return services.AddDandyRabbitMQSerialization(configuration);
    }

    public static IServiceCollection AddDandyRabbitMQSerialization(this IServiceCollection services, SerializationConfiguration configuration)
    {
        if (services.BuildServiceProvider().GetService(typeof(IPayloadSerializer)) != null)
            return services;

        if (!configuration.IsInitialized())
            throw new InvalidOperationException("Payload serializer type is not set.");

        services.AddSingleton(typeof(IPayloadSerializer), configuration.PayloadSerializerType);
        if (configuration.PayloadSerializerConfiguration != null)
            services.AddSingleton(configuration.PayloadSerializerConfiguration.GetType(), configuration.PayloadSerializerConfiguration);

        services.AddSingleton(configuration);

        return services;
    }
}