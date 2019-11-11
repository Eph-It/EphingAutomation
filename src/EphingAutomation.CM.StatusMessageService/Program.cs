using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.HttpSys;
using EphingAutomation.Logging;

namespace EphingAutomation.CM.StatusMessageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var loggerConfig = new EALogging();
            loggerConfig.Configure("StatusMessageService");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseHttpSys(options =>
                    {
                        options.Authentication.Schemes = AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                        options.Authentication.AllowAnonymous = false;
                    });
                });
            if (!Environment.UserInteractive)
            {
                hostBuilder.UseWindowsService();
            }
            return hostBuilder;
        }
    }
}
