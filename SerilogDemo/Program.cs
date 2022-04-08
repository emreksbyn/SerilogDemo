using Example.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Formatting;
using System.IO;

namespace SerilogDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            //Logger logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration, sectionName: "CustomSection")
            //    .CreateLogger();
            //logger.Information("Start");

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration,sectionName:"CustomSection")
            //    .WriteTo.File(new JsonFormatter(), "./Logs/mylogs.json")
            //    .CreateLogger();



            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfiguration)=>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                });
    }
}