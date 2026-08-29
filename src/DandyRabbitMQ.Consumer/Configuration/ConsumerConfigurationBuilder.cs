using System.Reflection;
using DandyRabbitMQ.Core.Connectivity;
using DandyRabbitMQ.Serialization;

namespace DandyRabbitMQ.Consumer.Configuration;

public sealed class ConsumerConfigurationBuilder
{
    private readonly ConsumerConfiguration _configuration = new();

    public ConsumerConfigurationBuilder UseTypeFactory(Func<string, Type> factory)
    {
        _configuration.TypeFactory = factory;
        return this;
    }

    public ConsumerConfigurationBuilder UseTypeNameFactory(Type interceptorType)
    {
        _configuration.ConsumerInterceptorType = interceptorType;
        return this;
    }

    public ConsumerConfigurationBuilder ScanInAssemblies(params Assembly[] assemblies)
    {
        _configuration.Assemblies = assemblies;
        return this;
    }

    public ConsumerConfigurationBuilder SubscribeChannel(Action<ChannelConfiguration> channelAction)
    {
        var channel = new ChannelConfiguration();
        channelAction(channel);

        _configuration.Channels = _configuration.Channels != null
            ? _configuration.Channels.Concat([channel]).ToArray()
            : [channel];

        return this;
    }

    public ConsumerConfigurationBuilder OnExceptionWhenInitializingWorker(Action<IServiceProvider, Exception> handler)
    {
        _configuration.OnExceptionWhenInitializingWorker = handler;
        return this;
    }

    public ConsumerConfigurationBuilder OnExceptionWhenReceivingMessage(Action<IServiceProvider, Exception> handler)
    {
        _configuration.OnExceptionWhenReceivingMessage = handler;
        return this;
    }

    public ConsumerConfigurationBuilder OnExceptionWhenAckOrNack(Action<IServiceProvider, Exception> handler)
    {
        _configuration.OnExceptionWhenAckOrNack = handler;
        return this;
    }

    public ConsumerConfigurationBuilder OnExceptionWhenIntercepting(Action<IServiceProvider, Exception> handler)
    {
        _configuration.OnExceptionWhenIntercepting = handler;
        return this;
    }

    public SerializationConfigurationBuilder Serialization { get; set; } = new();
    public ConnectivityConfigurationBuilder Connectivity { get; set; } = new();

    internal ConsumerConfiguration Build()
    {
        _configuration.SerializationConfigurationBuilder = Serialization;
        _configuration.ConnectivityConfigurationBuilder = Connectivity;

        return _configuration;
    }
}