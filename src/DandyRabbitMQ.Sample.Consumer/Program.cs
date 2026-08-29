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

builder.Services.AddDandyRabbitMQConsumer(cfg =>
{
    cfg.Connectivity.ConnectToCluster("dev", "dev", [new Uri("localhost:5672"), new Uri("localhost:5673")], recoveryInterval: null);
    cfg.Connectivity.OnConnectionException((_, ex) => Console.WriteLine($"Exception occurred: {ex}"));
    cfg.Serialization.UseSystemTextJson();

    cfg.ScanInAssemblies(Assembly.Load("DandyRabbitMQ.Sample.Consumer"));
    cfg.SubscribeChannel(channel =>
    {
        channel.Exchange.Name = "messages";
        channel.Queue.Name = "consumer-1";
        channel.Queue.RoutingKeys = ["all"];
    });
});

Console.WriteLine("...done!");
Console.WriteLine("Starting...");

builder.Build().Run();