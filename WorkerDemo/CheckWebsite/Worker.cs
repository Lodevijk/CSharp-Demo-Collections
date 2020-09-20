using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CheckWebsite
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            client.Timeout = new TimeSpan(0,0,3);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            _logger.LogInformation("Service was stopped");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await client.GetAsync("https://google.com");
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Status is ok. Code: {StatusCode}", result.StatusCode);
                }
                else
                {
                    _logger.LogError("Status not ok. Code: {StatusCode}", result.StatusCode);
                }
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
