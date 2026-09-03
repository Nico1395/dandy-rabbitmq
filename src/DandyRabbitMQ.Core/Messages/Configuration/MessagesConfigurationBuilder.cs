using System.Collections.Concurrent;
using System.Reflection;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessagesConfigurationBuilder
{
    private readonly Dictionary<Type, MessageConfiguration> _messagesByRuntimeType = new();
    private readonly Dictionary<string, MessageConfiguration> _messagesByKey = new();
    private Assembly[] _assemblies = [];

    public MessagesConfigurationBuilder()
    {
    }

    public MessagesConfigurationBuilder(MessagesConfiguration? configuration)
    {
        if (configuration == null)
            return;

        _messagesByRuntimeType = new Dictionary<Type, MessageConfiguration>(configuration.MessagesByRuntimeType);
        _assemblies = configuration.Assemblies;
    }

    public MessagesConfigurationBuilder AddMessage(Type messageType, Action<MessageConfigurationBuilder> builderAction)
    {
        var builder = new MessageConfigurationBuilder(messageType);
        builderAction.Invoke(builder);
        var message = builder.Build();

        _messagesByRuntimeType[message.RuntimeType] = message;
        _messagesByKey[message.Key] = message;

        return this;
    }

    public MessagesConfigurationBuilder ScanInAssemblies(params Assembly[] assemblies)
    {
        _assemblies = assemblies;
        return this;
    }

    public MessagesConfiguration Build()
    {
        return new MessagesConfiguration
        {
            MessagesByRuntimeType = new ConcurrentDictionary<Type, MessageConfiguration>(_messagesByRuntimeType),
            MessagesByKey = new ConcurrentDictionary<string, MessageConfiguration>(_messagesByKey),
            Assemblies = _assemblies,
        };
    }
}