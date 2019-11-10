using EphingAutomation.Models.ConfigMgr;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EphingAutomation.CM.StatusMessageProcessorService.Repository
{
    public interface IProcessStatusMessage
    {
        Task StartProcessingAsync(StatusMessage message);
    }
}
