using System;
using System.Threading;
using System.Threading.Tasks;
using Feature.BlazorServer.Data;
using Feature.Shared;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Feature.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string[] features = { "ASP.NET Core", "Blazor", "C#", "Dapper", "EF Core", "Facebook", "Google", "Microsoft" };
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var context = new ApplicationDbContext())
                {
                    FeatureModel model = new FeatureModel { Title = features[DateTime.Now.Minute % 8], Created = DateTime.Now };
                    context.Features.Add(model);
                    context.SaveChanges(); 
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60_000, stoppingToken);
            }
        }
    }
}
