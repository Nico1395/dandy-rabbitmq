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

        string[]? targetRoutingKeys = null;
        Type? runtimeType = null;

        // We can't send a message without knowing the exchange and at least one routing keys. If either is missing,
        // we need to resolve the message configuration and fill in missing information.

        if (exchange == null || routingKeys == null)
        {
            if (!messagesConfiguration.MessagesByRuntimeType.TryGetValue(message.GetType(), out var messageConfiguration))
                throw new InvalidOperationException($"Failed to resolve message configuration for type '{message.GetType()}'.");

            runtimeType = messageConfiguration.RuntimeType;
            exchange ??= messageConfiguration.Exchange;
            targetRoutingKeys = routingKeys != null ? routingKeys.ToArray() : messageConfiguration.RoutingKeys;
            properties.Type = messageConfiguration.Key;
        }

        runtimeType ??= message.GetType();
        properties.Type ??= runtimeType.Name;

        if (targetRoutingKeys == null)
            throw new InvalidOperationException("Failed to resolve routing keys. None were provided and none were configured.");

        targetRoutingKeys = targetRoutingKeys.Where(routingKey => !string.IsNullOrWhiteSpace(routingKey)).Distinct().ToArray();
        if (targetRoutingKeys.Length == 0)
            throw new InvalidOperationException("Routing keys were resolved, but they are empty.");

        if (string.IsNullOrWhiteSpace(exchange))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(exchange));

        return new DispatchInfo(runtimeType, exchange, targetRoutingKeys, properties);
    }
}