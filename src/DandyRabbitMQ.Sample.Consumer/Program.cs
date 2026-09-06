using System.Reflection;
using DandyRabbitMQ.Consumer.Configuration;
using DandyRabbitMQ.Serialization.SystemTextJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Initializing...");

var settings = new HostApplicationBuilderSettings
{
    Args = args,
    Configuration = new ConfigurationManager(),
    ContentRootPath = Directory.GetCurrentDirectory(),
};

settings.Configuration.AddJsonFile("appsettings.json", optional: false);

var builder = Host.CreateApplicationBuilder(settings);
var assemblies = new [] { Assembly.Load("DandyRabbitMQ.Sample.Consumer"), Assembly.Load("DandyRabbitMQ.Sample.Shared") };

builder.Services.AddDandyRabbitMQConsumer(cfg =>
{
    cfg.Connectivity.ConnectToCluster("dev", "dev", [new Uri("localhost:5672"), new Uri("localhost:5673")], recoveryInterval: null);
    cfg.Connectivity.OnConnectionException((_, ex) => Console.WriteLine($"Exception occurred: {ex}"));
    cfg.Serialization.UseSystemTextJson();
    cfg.Declarations.SubscribeChannel("messages", "consumer-1", channel =>
    {
        channel.Queue.RoutingKeys = ["all"];
    });

    cfg.ScanInAssemblies(assemblies);
});

Console.WriteLine("...done!");
Console.WriteLine("Starting...");

builder.Build().Run();