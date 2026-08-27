using System.Collections.Concurrent;

namespace DandyRabbitMQ.Core.Messages.Types;

public static class MessageTypeNameMap
{
    private static readonly ConcurrentDictionary<Type, string> _types = [];
    private static readonly ConcurrentDictionary<string, Type> _names = [];

    public static string GetName(Type messageType)
    {
        return _types.GetOrAdd(messageType, type =>
        {
            var name = type.FullName ?? throw new InvalidOperationException($"Failed to resolve the type name of message {type}.");
            _names[name] = type;

            return name;
        });
    }

    public static Type GetType(string messageTypeName)
    {
        return _names.GetOrAdd(messageTypeName, name =>
        {
            var type = Type.GetType(name) ?? throw new InvalidOperationException($"Failed to resolve the type of message {name}.");
            _types[type] = name;

            return type;
        });
    }

    // private static string? GetIdentifiableName(this Type type)
    // {
    //     if (!type.IsGenericType)
    //         return type.FullName;
    //
    //     var genericTypeDefinition = type.GetGenericTypeDefinition();
    //     var genericArguments = string.Join(", ", type.GetGenericArguments().Select(a => a.GetIdentifiableName()));
    //
    //     return $"{genericTypeDefinition.FullName}[[{genericArguments}]]";
    // }
}