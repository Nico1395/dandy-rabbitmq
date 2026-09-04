using Microsoft.Extensions.DependencyInjection;

namespace DandyRabbitMQ.Serialization.NewtonsoftJson.Tests.Fixtures;

public sealed class DefaultFixture : IServiceProvider
{
    private readonly IServiceProvider _serviceProvider;
    
    public DefaultFixture()
    {
        var services = new ServiceCollection();

        services.AddDandyRabbitMQSerialization(config => config.UseNewtonsoftJson());

        _serviceProvider = services.BuildServiceProvider();
    }

    public object? GetService(Type serviceType)
    {
        return _serviceProvider.GetService(serviceType);
    }
    
    public IPayloadSerializer GetPayloadSerializer()
    {
        return _serviceProvider.GetRequiredService<IPayloadSerializer>();
    }
}