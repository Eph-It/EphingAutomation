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

        public void Configure(string LogName)
        {
            var path = Path.Combine(_environment.ContentRootPath ?? _environment.WebRootPath, "Logs" ,$"{LogName}.log");
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
