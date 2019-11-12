using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.ServiceProcess;
using Serilog;
using EphingAutomation.Logging;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using EphingAutomation.CM.StatusMessageDB.Model;
using EphingAutomation.CM.StatusMessageDB;

namespace EphingAutomation.CM.StatusMessageReceiver
{
    class Program
    {
        public static IConfigurationRoot GetConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args);

            return builder.Build();
        }
        static void Main(string[] args)
        {
            var loggerConfig = new EALogging();
            loggerConfig.Configure("StatusMessageReceiver");
            // Log.Information("Started with parameters {@args}", args);
            IConfigurationRoot configuration = null;
            try
            {
                configuration = GetConfiguration(args);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error getting configuration");
                throw;
            }
            var smObject = new StatusMessage();
            smObject.MessageId = int.Parse(configuration["MessageId"]);
            smObject.InsString1 = configuration["InsString1"];
            smObject.InsString2 = configuration["InsString2"];
            smObject.InsString3 = configuration["InsString3"];
            smObject.InsString4 = configuration["InsString4"];
            smObject.InsString5 = configuration["InsString5"];
            smObject.InsString6 = configuration["InsString6"];
            smObject.InsString7 = configuration["InsString7"];
            smObject.InsString8 = configuration["InsString8"];
            smObject.InsString9 = configuration["InsString9"];
            smObject.InsString10 = configuration["InsString10"];
            smObject.Recorded = DateTime.UtcNow;

            string dbPath = configuration["StatusMessageDb"];
            var db = new StatusMessageDBContext(dbPath);

            var jobRepo = new StatusMessageDB.Repository.JobRepository(db);
            var smRepo = new StatusMessageDB.Repository.StatusMessageRepository(db);

            smRepo.New(smObject);

            var openJobs = jobRepo.GetActiveJobs();
            if(openJobs.Count == 0)
            {
                // logic to open a job
            }

            /*IServiceRepository serviceRepo = new ServiceRepository();
            IStatusMessageReceiverErrorHandling statMessageErrorHandling = new StatusMessageReceiverErrorHandling();
            var smObject = new StatusMessage()
            {
                RequestId = Guid.NewGuid(),
                Arguments = args
            };
            try
            {
                serviceRepo.StartEAService();
                serviceRepo.SendArgs(smObject);
            }
            catch
            {
                Thread.Sleep(5000);
                serviceRepo.SendArgs(smObject);
                statMessageErrorHandling.SaveErrors(smObject);
            }

            statMessageErrorHandling.ProcessPreviousErrors();
            */
        }
    }
}
