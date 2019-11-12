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

namespace EphingAutomation.CM.StatusMessageReceiver
{
    class Program
    {
        public static Dictionary<string, string> ProcessArguments(string[] args)
        {
            var returnDictionary = new Dictionary<string, string>();
            bool argIsValue = false;
            for (int i = 0; i < args.Count(); i++)
            {
                if (!argIsValue)
                {
                    string arg = args[i];
                    if (!String.IsNullOrEmpty(arg))
                    {
                        if (arg.StartsWith('-') || arg.StartsWith('/'))
                        {
                            string key = arg.TrimStart('-').TrimStart('-').TrimStart('/');
                            returnDictionary.Add(key, args[i + 1]);
                        }
                    }
                }
                else
                {
                    argIsValue = false;
                }
            }
            return returnDictionary;
        }
        static void Main(string[] args)
        {
            var loggerConfig = new EALogging();
            loggerConfig.Configure("StatusMessageReceiver");
            Log.Information("Started with parameters {@args}", args);
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
