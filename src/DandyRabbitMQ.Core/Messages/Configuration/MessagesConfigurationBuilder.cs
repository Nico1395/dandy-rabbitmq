using System.Collections.Concurrent;
using System.Reflection;

namespace DandyRabbitMQ.Core.Messages.Configuration;

public sealed class MessagesConfigurationBuilder
{
    private readonly Dictionary<Type, MessageConfiguration> _messages = new();
    private Assembly[]? _assemblies;

    public MessagesConfigurationBuilder()
    {
    }

    public MessagesConfigurationBuilder(MessagesConfiguration? configuration)
    {
        if (configuration == null)
            return;

        _messages = new Dictionary<Type, MessageConfiguration>(configuration.MessagesByRuntimeType);
        _assemblies = configuration.Assemblies;
    }

    public MessagesConfigurationBuilder AddMessage(Type messageType, Action<MessageConfigurationBuilder> builderAction)
    {
        var builder = new MessageConfigurationBuilder(messageType);
        builderAction.Invoke(builder);

        var message = builder.Build();
        _messages[message.RuntimeType] = message;

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
            MessagesByRuntimeType = new ConcurrentDictionary<Type, MessageConfiguration>(_messages),
            Assemblies = _assemblies,
        };
    }
}