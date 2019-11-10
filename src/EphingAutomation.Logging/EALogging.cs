using Microsoft.AspNetCore.Hosting;
using System;
using Serilog;

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
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("EA.CM.StatusMessageReceiver.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
