using System.Reflection;
using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Consumer.Configuration;

public sealed class ConsumerConfiguration
{
    public Func<string, Type>? TypeFactory { get; internal set; }
    public Assembly[]? Assemblies { get; internal set; }
    public ChannelConfiguration[]? Channels { get; internal set; }

    public Action<IServiceProvider, Exception>? OnExceptionWhenInitializingWorker { get; internal set; }
    public Action<IServiceProvider, Exception>? OnExceptionWhenReceivingMessage { get; internal set; }

    public Action<SerializationConfigurationBuilder>? SerializationConfigurationBuilderAction { get; internal set; }
    public Action<ConnectivityConfigurationBuilder>? ConnectivityConfigurationBuilderAction { get; internal set; }
}