using EphingAutomation.Models.ConfigMgr;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageReceiver.Repository
{
    public interface IStatusMessageReceiverErrorHandling
    {
        void SaveErrors(StatusMessage smObject);
        void ProcessPreviousErrors();
    }
}
