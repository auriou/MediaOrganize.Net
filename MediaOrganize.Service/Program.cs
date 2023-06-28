using MediaOrganize.Service.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, configuration) =>
    {
        //context.InitAppsettings(configuration);
    })
    .ConfigureServices((hostContext, services) =>
    {
        //services.AddHostedService<WorkerHostedService>(service =>
        //{
        //    return new WorkerHostedService(args);
        //});
        services.AddHostedService<WorkerHostedService>();
    })
    .RunConsoleAsync();