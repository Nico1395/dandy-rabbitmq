using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Connectivity;

public static class ConnectivityDependencyInjection
{
    /// <summary>
    /// Adds DandyRabbitMQ connectivity services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         If the <see cref="IConnectionProvider"/> is already registered, the method acts idempotent and does nothing.
    ///     </para>
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which RabbitMQ connectivity services are added.</param>
    /// <param name="connectionAction">An optional action used to configure the RabbitMQ connection using the <see cref="RabbitMQConnectionConfigurationBuilder"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the RabbitMQ connectivity services added.</returns>
    public static IServiceCollection AddDandyRabbitMQConnectivity(this IServiceCollection services, Action<RabbitMQConnectionConfigurationBuilder> connectionAction)
    {
        if (services.BuildServiceProvider().GetService(typeof(IConnectionProvider)) != null)
            return services;

        var builder = new RabbitMQConnectionConfigurationBuilder();
        connectionAction.Invoke(builder);
        var connectionConfiguration = builder.Build();

        services.AddSingleton(connectionConfiguration);
        services.AddSingleton<IConnectionProvider, ConnectionProvider>();

        return services;
    }
}