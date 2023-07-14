using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MediaOrganize.Service;
using Microsoft.Extensions.Options;
using MediaOrganize.Models;

namespace MediaOrganize.Service.Workers
{
    internal class WorkerHostedService : IHostedService
    {
        private Config _config;

        public WorkerHostedService(
            IServiceProvider serviceProvider,
            IHostApplicationLifetime appLifetime,
            IOptions<Config> options)
        {
            _config = options.Value;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbSearch = new ThemoviedbSearch(_config.Tmdb.Language, _config.Tmdb.Token);

            var directoryScan = new DirectoryScan(
                dbSearch, _config.ScanPath, 
                _config.MoviePath, _config.SeriePath, _config.ScanFilter);

            await directoryScan.ScanAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
