using EphingAutomation.Models.ConfigMgr;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.ServiceProcess;
using System.Text;

namespace EphingAutomation.CM.StatusMessageReceiver.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        public void StartEAService(bool AlreadyRan = false)
        {
            ServiceController sc = new ServiceController("EA.CM.StatusMessageProcessorService");
            if (sc.Status == ServiceControllerStatus.Running) { return; }
            if (AlreadyRan)
            {
                throw new Exception("Error starting service to process status message.");
            }
            switch (sc.Status)
            {
                case ServiceControllerStatus.Stopped:
                    sc.Start();
                    break;
                case ServiceControllerStatus.StartPending:
                    break;
                case ServiceControllerStatus.StopPending:
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(100000000));
                    sc.Start();
                    break;
                case ServiceControllerStatus.Running:
                    return;
                case ServiceControllerStatus.PausePending:
                case ServiceControllerStatus.Paused:
                case ServiceControllerStatus.ContinuePending:
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(100000000));
                    sc.Start();
                    break;
            }
            sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(100000000)); // waits up to 10 seconds - should never take this long
            StartEAService(true);
        }
        public void SendArgs(StatusMessage smObject)
        {
            using(var pipe = new NamedPipeClientStream(".", "EphingAdmin.CM.StatusMessages", PipeDirection.InOut, PipeOptions.None))
            {
                try
                {
                    pipe.Connect(10000);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error connecting to pipe, will restart service - {e.Message}");
                    StartEAService();
                    pipe.Connect(10000);
                }
                if (!pipe.IsConnected)
                {
                    throw new Exception("Pipe is not connected!");
                }
                Serializer.Serialize(pipe, smObject);
                pipe.WaitForPipeDrain();
                pipe.Dispose();
            }
        }
    }
}
