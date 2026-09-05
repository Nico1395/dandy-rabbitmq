using DandyRabbitMQ.Core.Messages.Configuration;
using RabbitMQ.Client;

namespace DandyRabbitMQ.Producer;

public readonly struct DispatchInfo(Type runtimeType, string exchange, string[] routingKeys, BasicProperties properties)
{
    public Type RuntimeType { get; } = runtimeType;
    public string Exchange { get; } = exchange;
    public string[] RoutingKeys { get; } = routingKeys;
    public BasicProperties Properties { get; } = properties;

    public static DispatchInfo Create(MessagesConfiguration messagesConfiguration, string? exchange, IEnumerable<string>? routingKeys, object message, BasicProperties? properties)
    {
        properties ??= new BasicProperties();

        var messageType = message.GetType();
        var messageConfiguration = messagesConfiguration.MessagesByRuntimeType.GetValueOrDefault(messageType);

        properties.Type ??= messageConfiguration?.Key ?? messageType.Name;

        string[]? targetRoutingKeys = null;
        if (exchange == null || routingKeys == null)
        {
            if (messageConfiguration == null)
                throw new InvalidOperationException($"Failed to resolve message configuration for type '{message.GetType()}'.");

            targetRoutingKeys = routingKeys != null ? routingKeys.ToArray() : messageConfiguration.RoutingKeys;
            exchange ??= messageConfiguration.Exchange;
        }

        if (string.IsNullOrWhiteSpace(exchange))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(exchange));

        if (targetRoutingKeys == null)
            throw new InvalidOperationException("Failed to resolve routing keys. None were provided and none were configured.");

        targetRoutingKeys = targetRoutingKeys.Where(routingKey => !string.IsNullOrWhiteSpace(routingKey)).Distinct().ToArray();
        if (targetRoutingKeys.Length == 0)
            throw new InvalidOperationException("Routing keys were resolved, but they are empty.");

        return new DispatchInfo(
            messageConfiguration?.RuntimeType ?? messageType,
            exchange,
            targetRoutingKeys,
            properties);
    }
}