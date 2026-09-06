using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Declarations.Configuration;
using DandyRabbitMQ.Core.Encoding.Configuration;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Producer;

/// <summary>
/// Provides dependency injection registration extensions for the producer.
/// </summary>
public static class ProducerServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures DandyRabbitMQ producer <paramref name="services"/>.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <param name="builderAction">An action that configures the producer.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDandyRabbitMQProducer(this IServiceCollection services, Action<ProducerConfigurationBuilder> builderAction)
    {
        var builder = new ProducerConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        services.AddSingleton(configuration);
        services.AddScoped<IProducer, Producer>();

        services.AddDandyRabbitMQConnectivity(configuration.ConnectivityConfigurationBuilder.Build());
        services.AddDandyRabbitMQSerialization(configuration.SerializationConfigurationBuilder.Build());
        services.AddDandyRabbitMQMessages(configuration.MessagesConfigurationBuilder.Build());
        services.AddDandyRabbitMQEncoding(configuration.EncodingConfigurationBuilder.Build());
        services.AddDandyRabbitMQDeclarations(configuration.DeclarationsConfiguration.Build());

        return services;
    }
}
