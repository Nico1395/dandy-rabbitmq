using RabbitMQ.Client;

namespace DandyRabbitMQ.Connectivity;

/// <summary>
/// Builder for <see cref="RabbitMQConnectionConfiguration"/>.
/// </summary>
public sealed class RabbitMQConnectionConfigurationBuilder
{
    private readonly RabbitMQConnectionConfiguration _configuration = new();

    /// <summary>
    /// Facade to configure a connection to a cluster of multiple RabbitMQ <paramref name="nodes"/> using <paramref name="userName"/>
    /// and <paramref name="password"/>.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Enables <see cref="ConnectionFactory.AutomaticRecoveryEnabled"/> and <see cref="ConnectionFactory.TopologyRecoveryEnabled"/>
    ///         to create a fail-over connection to the cluster as well.
    ///     </para>
    /// </remarks>
    /// <param name="userName">Username for the nodes.</param>
    /// <param name="password">Password for the <paramref name="userName"/>.</param>
    /// <param name="recoveryInterval">Amount of time the client will wait for before re-trying to recover connection.</param>
    /// <param name="nodes"><see cref="Uri"/>s of the nodes.</param>
    /// <returns>The builder.</returns>
    public RabbitMQConnectionConfigurationBuilder ConnectToCluster(string userName, string password, IEnumerable<Uri> nodes, TimeSpan? recoveryInterval)
    {
        _configuration.ConnectionFactoryInternal.UserName = userName;
        _configuration.ConnectionFactoryInternal.Password = password;
        _configuration.ConnectionFactoryInternal.AutomaticRecoveryEnabled = true;
        _configuration.ConnectionFactoryInternal.TopologyRecoveryEnabled = true;
        _configuration.ConnectionFactoryInternal.NetworkRecoveryInterval = recoveryInterval ?? TimeSpan.FromSeconds(5);
        _configuration.NodesInternal = nodes.Select(uri => new AmqpTcpEndpoint(uri)).ToList();

        return this;
    }

    /// <summary>
    /// Allows freely configuring the underlying, internally used <see cref="ConnectionFactory"/>.
    /// </summary>
    /// <param name="factory">Action for configuring the <see cref="ConnectionFactory"/>.</param>
    /// <returns>The builder.</returns>
    public RabbitMQConnectionConfigurationBuilder ConnectionFactory(Action<ConnectionFactory> factory)
    {
        factory(_configuration.ConnectionFactoryInternal);
        return this;
    }
    
    /// <summary>
    /// Allows overwriting the underlying, internally used <see cref="ConnectionFactory"/>.
    /// </summary>
    /// <param name="factory">The <see cref="ConnectionFactory"/>.</param>
    /// <returns>The builder.</returns>
    public RabbitMQConnectionConfigurationBuilder SetConnectionFactory(ConnectionFactory factory)
    {
        _configuration.ConnectionFactoryInternal = factory;
        return this;
    }

    /// <summary>
    /// Configures what happens when an exception occurs during the process of establishing a connection.
    /// </summary>
    /// <param name="action">The actions.</param>
    /// <returns>The builder.</returns>
    public RabbitMQConnectionConfigurationBuilder OnConnectionException(Action<IServiceProvider, Exception> action)
    {
        _configuration.OnConnectionException = action;
        return this;
    }

    internal RabbitMQConnectionConfiguration Build() => _configuration;
}