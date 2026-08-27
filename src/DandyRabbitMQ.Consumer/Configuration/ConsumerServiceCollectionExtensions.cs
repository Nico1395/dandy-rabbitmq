using System.Reflection;
using DandyRabbitMQ.Consumer.Internal;
using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Consumer.Configuration;

public static class ConsumerServiceCollectionExtensions
{
    private static readonly IReadOnlyList<Type> _typesToRegister =
    [
        typeof(IConsumer<>),
        typeof(IConsumerMiddleware<>),
        typeof(IConsumerExceptionHandler<>),
    ];

    public static IServiceCollection AddDandyRabbitMQConsumer(this IServiceCollection services, Action<ConsumerConfigurationBuilder> builderAction)
    {
        var builder = new ConsumerConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        services.AddSingleton(configuration);
        services.AddHostedService<ConsumerWorker>();
        services.AddSingleton<IConsumerPipeline, ConsumerPipeline>();

        if (configuration.Assemblies != null)
            AddServicesFromAssemblies(services, configuration.Assemblies);

        if (configuration.ConnectivityConfigurationBuilderAction != null)
            services.AddDandyRabbitMQConnectivity(configuration.ConnectivityConfigurationBuilderAction);

        if (configuration.SerializationConfigurationBuilderAction != null)
            services.AddDandyRabbitMQSerialization(configuration.SerializationConfigurationBuilderAction);

        return services;
    }

    private static void AddServicesFromAssemblies(IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        var implementationTypes = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false });
        foreach (var implementationType in implementationTypes)
        {
            var interfaces = implementationType.ImplementedInterfaces;
            foreach (var @interface in interfaces)
            {
                if (!@interface.IsGenericType)
                    continue;

                var genericDefinition = @interface.GetGenericTypeDefinition();
                if (_typesToRegister.Contains(genericDefinition))
                    services.AddTransient(@interface, implementationType);
            }
        }
    }
}