using System.Reflection;
using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Core.Declarations.Configuration;
using DandyRabbitMQ.Core.Encoding.Configuration;
using DandyRabbitMQ.Core.Messages.Configuration;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Consumer.Configuration;

public sealed class ConsumerConfiguration
{
    internal SerializationConfigurationBuilder SerializationConfigurationBuilder { get; set; } = new();
    internal ConnectivityConfigurationBuilder ConnectivityConfigurationBuilder { get; set; } = new();
    internal MessagesConfigurationBuilder MessagesConfigurationBuilder { get; set; } = new();
    internal EncodingConfigurationBuilder EncodingConfigurationBuilder { get; set; } = new();
    internal DeclarationsConfigurationBuilder DeclarationsConfigurationBuilder { get; set; } = new();

    public Assembly[]? Assemblies { get; internal set; }
    public Type? ConsumerInterceptorType { get; internal set; }

    public Action<IServiceProvider, Exception>? OnExceptionWhenInitializingWorker { get; internal set; }
    public Action<IServiceProvider, Exception>? OnExceptionWhenReceivingMessage { get; internal set; }
    public Action<IServiceProvider, Exception>? OnExceptionWhenAckOrNack { get; internal set; }
    public Action<IServiceProvider, Exception>? OnExceptionWhenIntercepting { get; internal set; }
}