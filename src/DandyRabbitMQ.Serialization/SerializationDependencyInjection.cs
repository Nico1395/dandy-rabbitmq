using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Serialization;

/// <summary>
/// Registers DandyRabbitMQ serialization services with dependency injection.
/// </summary>
public static class SerializationDependencyInjection
{
    /// <summary>
    /// Adds and configures a payload serializer.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <param name="builderAction">An action that configures the serialization builder.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDandyRabbitMQSerialization(this IServiceCollection services, Action<SerializationConfigurationBuilder> builderAction)
    {
        var builder = new SerializationConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        return services.AddDandyRabbitMQSerialization(configuration);
    }

    /// <summary>
    /// Adds a configured payload serializer.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <param name="configuration">The serialization configuration to register.</param>
    /// <returns>The updated service collection.</returns>
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
