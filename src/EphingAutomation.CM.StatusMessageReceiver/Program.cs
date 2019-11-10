using EphingAutomation.CM.StatusMessageReceiver.Repository;
using EphingAutomation.Models.ConfigMgr;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.ServiceProcess;

namespace EphingAutomation.CM.StatusMessageReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceRepository serviceRepo = new ServiceRepository();
            IStatusMessageReceiverErrorHandling statMessageErrorHandling = new StatusMessageReceiverErrorHandling();
            var smObject = new StatusMessage()
            {
                RequestId = new Guid(),
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
