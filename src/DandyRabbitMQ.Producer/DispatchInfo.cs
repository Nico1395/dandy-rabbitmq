using DandyRabbitMQ.Core.Messages.Configuration;
using RabbitMQ.Client;

namespace DandyRabbitMQ.Producer;

public readonly struct DispatchInfo
{
    private DispatchInfo(Type runtimeType, string exchange, string[] routingKeys, BasicProperties properties)
    {
        RuntimeType = runtimeType;
        Exchange = exchange;
        RoutingKeys = routingKeys;
        Properties = properties;
    }

    public Type RuntimeType { get; }
    public string Exchange { get; }
    public string[] RoutingKeys { get; }
    public BasicProperties Properties { get; }

    public static DispatchInfo Create(MessagesConfiguration messagesConfiguration, string? exchange, IEnumerable<string>? routingKeys, object message, BasicProperties? properties)
    {
        properties ??= new BasicProperties();

        var messageType = message.GetType();
        var messageConfiguration = messagesConfiguration.MessagesByRuntimeType.GetValueOrDefault(messageType);

        // Determine message type
        properties.Type ??= messageConfiguration?.Key ?? messageType.Name;

        // Determine routing keys
        string[] targetRoutingKeys;
        if (routingKeys == null)
        {
            if (messageConfiguration == null)
                throw new InvalidOperationException($"Failed to resolve message configuration for type '{messageType}'. You should add message types via assembly scanning or manual configuration, even if they are decorated with attributes, so their metadata is cached.");

            targetRoutingKeys = messageConfiguration.RoutingKeys ?? throw new InvalidOperationException("Failed to resolve routing keys. None were provided and none were configured.");
        }
        else
        {
            targetRoutingKeys = routingKeys.ToArray();
        }

        targetRoutingKeys = targetRoutingKeys.Where(routingKey => !string.IsNullOrWhiteSpace(routingKey)).Distinct().ToArray();
        if (targetRoutingKeys.Length == 0)
            throw new InvalidOperationException("Routing keys were resolved, but they are empty.");

        // Determine exchange
        if (exchange == null)
        {
            if (messageConfiguration == null)
                throw new InvalidOperationException($"Failed to resolve message configuration for type '{messageType}'. You should add message types via assembly scanning or manual configuration, even if they are decorated with attributes, so their metadata is cached.");

            exchange ??= messageConfiguration.Exchange;
        }

        if (string.IsNullOrWhiteSpace(exchange))
            throw new InvalidOperationException("Exchange cannot be null or whitespace.");

        // Determine runtime type
        var runtimeType = messageConfiguration?.RuntimeType ?? messageType;

        return new DispatchInfo(runtimeType, exchange, targetRoutingKeys, properties);
    }
}