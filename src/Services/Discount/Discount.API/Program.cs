using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using Ordering.API.Extensions;
//using Ordering.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Discount.API.Data;
using Discount.API.Extensions;

namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args)
                 .Build();

            host.MigrateDatabase<DiscountContext>((context, services) =>
              {
                  var logger = services.GetService<ILogger<DiscountContextSeed>>();
                  logger.LogInformation("main is called context");
                  DiscountContextSeed
                      .SeedAsync(context, logger)
                      .Wait();
              });
             host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
