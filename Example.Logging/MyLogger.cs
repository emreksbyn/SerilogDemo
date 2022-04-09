using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.IO;

namespace Example.Logging
{
    public static class MyLogger
    {
        public static void CreateLogger()
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new CompactJsonFormatter(), configuration.GetSection("Serilog:WriteTo:0:Args:path").Value, rollingInterval: RollingInterval.Day)
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspnetCore", LogEventLevel.Warning)
                .Enrich.WithProperty("AppName", "SerilogDemo")
                .Enrich.With(new ThreadIdEnricher())
                .CreateLogger();
        }
    }
}