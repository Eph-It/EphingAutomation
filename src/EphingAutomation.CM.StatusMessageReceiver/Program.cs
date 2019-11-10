using EphingAutomation.CM.StatusMessageReceiver.Repository;
using EphingAutomation.Models.ConfigMgr;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.ServiceProcess;
using Serilog;

namespace EphingAutomation.CM.StatusMessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Log.Information("Started with parameters {@args}", args);
            IServiceRepository serviceRepo = new ServiceRepository();
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
                statMessageErrorHandling.SaveErrors(smObject);
                return;
            }

            statMessageErrorHandling.ProcessPreviousErrors();
        }
    }
}
