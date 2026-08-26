using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Producer;

public static class ProducerServiceCollectionExtensions
{
    public static IServiceCollection AddDandyRabbitMQProducer(this IServiceCollection services, Action<ProducerConfigurationBuilder> builderAction)
    {
        var builder = new ProducerConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        services.AddSingleton(configuration);
        services.AddScoped<IMessageProducer, MessageProducer>();

        if (configuration.ConnectivityConfigurationBuilderAction != null)
            services.AddDandyRabbitMQConnectivity(configuration.ConnectivityConfigurationBuilderAction);

        if (configuration.SerializationConfigurationBuilderAction != null)
            services.AddDandyRabbitMQSerialization(configuration.SerializationConfigurationBuilderAction);

        return services;
    }
}