using System;
using System.Threading;
using System.Threading.Tasks;
using Feature.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Feature.WorkerService
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
            //[1] 데이터베이스 연결 문자열 읽어오기 
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            string connectionString = configuration.GetSection("ConnectionString").Value;

            //[2] 컨텍스트 개체 생성 
            var option = new DbContextOptionsBuilder<FeatureContext>();
            option.UseSqlServer(connectionString);
            var context = new FeatureContext(option.Options);

            //[3] 샘플로 60초에 한번씩 문자열 데이터 입력 
            string[] features = { "ASP.NET Core", "Blazor", "C#", "Dapper", "EF Core", "Facebook", "Google", "Microsoft" };
            while (!stoppingToken.IsCancellationRequested)
            {
                FeatureModel model = new FeatureModel { Title = features[DateTime.Now.Minute % 8], Created = DateTime.Now };
                context.Features.Add(model);
                context.SaveChanges();

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(60_000, stoppingToken);
            }
        }
    }
}
