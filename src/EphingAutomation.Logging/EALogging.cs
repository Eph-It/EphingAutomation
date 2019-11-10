using System;
using Serilog;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace EphingAutomation.Logging
{
    public class EALogging
    {
        private IHostEnvironment _environment;
        public EALogging(IHostEnvironment environment)
        {
            _environment = environment;
        }
        public EALogging()
        {
            
        }

        public void Configure(string LogName)
        {
            string path;
            if(_environment != null)
            {
                path = Path.Combine(_environment.ContentRootPath, "Logs", $"{LogName}.log");
                path = Path.Combine("Logs", $"{LogName}.log");
            }
            else
            {
                path = Path.Combine("Logs", $"{LogName}.log");
            }
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Environment content root {@_environment}", _environment);
        }
    }
}
