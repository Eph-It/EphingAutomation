using System;
using System.IO;
using EphingAutomation.CM.StatusMessageDB;
using Microsoft.Extensions.Configuration;

namespace EphingAutomation.CM.StatusMessageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var db = new StatusMessageDBContext();
        }
    }
}
