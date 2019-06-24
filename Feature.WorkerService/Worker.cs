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
            //[1] �����ͺ��̽� ���� ���ڿ� �о���� 
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            string connectionString = configuration.GetSection("ConnectionString").Value;

            //[2] ���ؽ�Ʈ ��ü ���� 
            var option = new DbContextOptionsBuilder<FeatureContext>();
            option.UseSqlServer(connectionString);
            var context = new FeatureContext(option.Options);

            //[3] ���÷� 60�ʿ� �ѹ��� ���ڿ� ������ �Է� 
            string[] features = { "ASP.NET Core", "Blazor", "C#", "Dapper", "EF Core", "Facebook", "Google", "Microsoft" };
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("������ ����: {time}", DateTimeOffset.Now);

                FeatureModel model = new FeatureModel { Title = features[DateTime.Now.Minute % 8], Created = DateTime.Now };
                context.Features.Add(model); // 1�п� �ϳ��� ������ ����
                context.SaveChanges();

                await Task.Delay(60_000, stoppingToken);
            }
        }
    }
}
