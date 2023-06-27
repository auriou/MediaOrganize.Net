
using MediaOrganize.Net;
using MediaOrganize.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var filePath = "C:\\Temp\\media";
var outputPath = "C:\\Temp\\media\\out\\";
var token = Environment.GetEnvironmentVariable("token");

var dbSearch = new ThemoviedbSearch("fr-FR", token);

var directoryScan = new DirectoryScan(dbSearch, filePath, $"{outputPath}movies", $"{outputPath}series");

await directoryScan.ScanAsync();

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, configuration) =>
    {
        //context.InitAppsettings(configuration);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<WorkerHostedService>(service =>
        {
            return new WorkerHostedService(args);
        });
        services.AddHostedService<WorkerHostedService>();
        //services.AddSrpCacheConfig(hostContext.HostingEnvironment, hostContext.Configuration);
        //services.AddSrpLoggerConfig(hostContext.HostingEnvironment, hostContext.Configuration);

        //services.AddHostedService<WorkerHostedService>();
        //services.AddSingleton<WorkerLoggerElastic>();
        //services.AddOptions<SrpBrokerConfig>().Bind(hostContext.Configuration.GetSection("kafkaReturnLabelCreate"));
        //services.AddOptions<APIDocumentSetting>().Bind(hostContext.Configuration.GetSection("ServiceApiDocument"));
        //services.AddSingleton<BdrWorker>();
        //services.AddSingleton<ServiceGenerateBdr>();
        //services.AddSingleton<ServiceManageBdrDocument>();
        //services.AddSrpCoreDependencyInjection(hostContext.Configuration);
        //services.AddSingleton<RepoBdr>();
        //services.AddSingleton<ServiceApiDocument>();
    })
    .RunConsoleAsync();
