using System.Reflection;
using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Consumer.Configuration;

public sealed class ConsumerConfiguration
{
    public Func<string, Type>? TypeFactory { get; internal set; }
    public Assembly[]? Assemblies { get; internal set; }
    public ChannelConfiguration[]? Channels { get; internal set; }
    public Type? ConsumerInterceptorType { get; internal set; }

    public Action<IServiceProvider, Exception>? OnExceptionWhenInitializingWorker { get; internal set; }
    public Action<IServiceProvider, Exception>? OnExceptionWhenReceivingMessage { get; internal set; }
    public Action<IServiceProvider, Exception>? OnExceptionWhenAckOrNack { get; internal set; }
    public Action<IServiceProvider, Exception>? OnExceptionWhenIntercepting { get; internal set; }

    public SerializationConfigurationBuilder SerializationConfigurationBuilder { get; set; } = new();
    public ConnectivityConfigurationBuilder ConnectivityConfigurationBuilder { get; set; } = new();
    public MessagesConfigurationBuilder MessagesConfigurationBuilder { get; set; } = new();
}