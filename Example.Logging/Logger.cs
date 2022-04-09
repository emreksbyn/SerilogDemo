﻿using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using System.IO;

namespace Example.Logging
{
    public static class Logger
    {
        public static void CreateLogger()
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("loggingSettings.json")
              .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File(new CompactJsonFormatter(), configuration.GetSection("Serilog:WriteTo:0:Args:path").Value)
                .CreateLogger();
        }
    }
}