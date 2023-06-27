using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganize.Net
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
