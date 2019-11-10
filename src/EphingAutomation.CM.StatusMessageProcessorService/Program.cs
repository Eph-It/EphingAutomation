using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EphingAutomation.CM.StatusMessageProcessorService.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;

namespace EphingAutomation.CM.StatusMessageProcessorService
{
    public static class Program
    {
 
        public static void Main(string[] args)
        {

            var hostBuilder = CreateHostBuilder(args);
            hostBuilder.Build().Run();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("EA.CM.StatusMessageProcessorService.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Starting service...");

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IProcessStatusMessage, ProcessStatusMessage>();
                    services.AddHostedService<Worker>();
                });
    }
}
