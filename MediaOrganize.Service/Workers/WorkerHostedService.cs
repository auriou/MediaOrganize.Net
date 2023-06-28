using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MediaOrganize.Service.Workers
{
    internal class WorkerHostedService : IHostedService
    {
        public WorkerHostedService(
            IServiceProvider serviceProvider,
            IHostApplicationLifetime appLifetime,
            IConfiguration configuration)
        {


        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
