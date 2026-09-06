namespace DandyRabbitMQ.Core.Declarations.Configuration;

public sealed class DeclarationsConfigurationBuilder
{
    private readonly DeclarationsConfiguration _configuration = new();

    public DeclarationsConfigurationBuilder SubscribeChannel(string exchange, string queue, Action<ChannelConfiguration> channelAction)
    {
        var channel = new ChannelConfiguration(exchange, queue);
        channelAction(channel);

        _configuration.Channels[queue] = channel;
        return this;
    }

    public DeclarationsConfiguration Build()
    {
        return _configuration;
    }
}