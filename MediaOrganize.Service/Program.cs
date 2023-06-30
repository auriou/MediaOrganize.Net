using MediaOrganize.Models;
using MediaOrganize.Service.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, configuration) =>
    {
        configuration.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<Config>(hostContext.Configuration);
        services.AddHostedService<WorkerHostedService>();
    })
    .StartAsync();