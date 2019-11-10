using EphingAutomation.Models.ConfigMgr;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageReceiver.Repository
{
    public interface IServiceRepository
    {
        void StartEAService(bool AlreadyRan = false);
        void SendArgs(StatusMessage smObject);
    }
}
