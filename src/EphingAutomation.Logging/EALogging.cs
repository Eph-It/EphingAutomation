using Microsoft.AspNetCore.Hosting;
using System;
using Serilog;
using System.IO;

namespace EphingAutomation.Logging
{
    public class EALogging
    {
        private IHostingEnvironment _environment;
        public EALogging(IHostingEnvironment environment)
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
                path = Path.Combine(_environment.ContentRootPath ?? _environment?.WebRootPath, "Logs", $"{LogName}.log");
            }
            else
            {
                path = Path.Combine("Logs", $"{LogName}.log");
            }
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
