using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public static class MessageServiceCollectionExtensions
{
    public static IServiceCollection AddDandyRabbitMQMessages(this IServiceCollection services, Action<MessagesConfigurationBuilder> builderAction)
    {
        var configuration = services.BuildServiceProvider().GetService<MessagesConfiguration>();
        var builder = new MessagesConfigurationBuilder(configuration);
        builderAction.Invoke(builder);
        configuration = builder.Build();

        return services.AddDandyRabbitMQMessages(configuration);
    }

    public static IServiceCollection AddDandyRabbitMQMessages(this IServiceCollection services, MessagesConfiguration configuration)
    {
        services.AddSingleton(configuration);
        ScanForMessages(configuration);

        return services;
    }

    private static void ScanForMessages(MessagesConfiguration configuration)
    {
        if (configuration.Assemblies.Length == 0)
            return;

        var messageTypes = configuration.Assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type.GetCustomAttributes<MessageAttribute>().Any() && type is
            {
                IsClass: true,
                IsAbstract: false,
                IsGenericTypeDefinition: false,
            })
            .ToArray();

        foreach (var messageType in messageTypes)
        {
            var metadata = new ConcurrentDictionary<string, object>();
            var messageConfiguration = configuration.Messages.GetOrAdd(messageType, type => new()
            {
                RuntimeType = type,
                Metadata = metadata,
            });

            var attributes = messageType.GetCustomAttributes<MessageAttribute>().ToArray();
            foreach (var attribute in attributes)
            {
                if (attribute is MessageExchangeAttribute exchangeAttribute)
                    messageConfiguration.Exchange = exchangeAttribute.Exchange;
                else if (attribute is MessageRoutesAttribute routingKeyAttribute)
                    messageConfiguration.RoutingKeys = routingKeyAttribute.Keys;
                else if (attribute is MessageMetadataAttribute metadataAttribute)
                    metadata[metadataAttribute.Key] = metadataAttribute.Value;
                else if (attribute is MessageTypeAttribute typeAttribute)
                    messageConfiguration.Type = typeAttribute.Type;
                else
                    throw new InvalidOperationException($"Unknown attribute type: {attribute.GetType()}");
            }
        }
    }
}