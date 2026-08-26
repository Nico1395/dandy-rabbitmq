using System.Collections.Concurrent;

namespace DandyRabbitMQ.Core.Messages.Types;

public static class MessageTypeNameProvider
{
    private static readonly ConcurrentDictionary<Type, string> _types = [];

    public static string Get(Type messageType)
    {
        return _types.GetOrAdd(messageType, type => type.GetIdentifiableName() ?? throw new InvalidOperationException($"Failed to resolve the type name of message {type}."));
    }

    private static string? GetIdentifiableName(this Type type)
    {
        if (!type.IsGenericType)
            return type.FullName;

        var genericTypeDefinition = type.GetGenericTypeDefinition();
        var genericArguments = string.Join(", ", type.GetGenericArguments().Select(a => a.GetIdentifiableName()));

        return $"{genericTypeDefinition.FullName}[[{genericArguments}]]";
    }
}