using System;
using Serilog;
using System.IO;

namespace EphingAutomation.Logging
{
    public class EALogging
    {
        
        public EALogging()
        {
            
        }

        public void Configure(string LogName)
        {
            string path = Environment.GetEnvironmentVariable("EphingAutomationLogDirectory");

            if(String.IsNullOrEmpty(path))
            {
                path = Path.Combine("Logs", $"{LogName}.log");
            }
            else
            {
                path = Path.Combine(path, $"{LogName}.log");
            }
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
