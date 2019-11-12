using System;
using System.IO;
using EphingAutomation.CM.StatusMessageDB;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EphingAutomation.CM.StatusMessageProcessor
{
    class Program
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
        static void Main(string[] args)
        {
            var loggingClass = new EphingAutomation.Logging.EALogging();
            loggingClass.Configure("StatusMessageProcessor");
            IConfigurationRoot configuration = null;
            try
            {
                configuration = GetConfiguration();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error getting configuration");
                throw;
            }

            string dbPath = configuration["StatusMessageDb"];
            var db = new StatusMessageDBContext(dbPath);

            var jobRepo = new StatusMessageDB.Repository.JobRepository(db);
            jobRepo.New();


            
            jobRepo.EndJob();
        }
    }
}
