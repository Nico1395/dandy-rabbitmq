using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Core.Declarations.Configuration;

public static class DeclarationsServiceCollectionExtensions
{
    public static IServiceCollection AddDandyRabbitMQDeclarations(this IServiceCollection services, Action<DeclarationsConfigurationBuilder> builderAction)
    {
        var builder = new DeclarationsConfigurationBuilder();
        builderAction.Invoke(builder);
        var configuration = builder.Build();

        return services;
    }

    public static IServiceCollection AddDandyRabbitMQDeclarations(this IServiceCollection services, DeclarationsConfiguration configuration)
    {
        if (services.BuildServiceProvider().GetService(typeof(IDeclarer)) != null)
            return services;

        services.AddSingleton(configuration);
        services.AddSingleton<IDeclarer, Declarer>();

        return services;
    }
}