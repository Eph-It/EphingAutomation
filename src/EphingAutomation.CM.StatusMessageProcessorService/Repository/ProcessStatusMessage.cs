﻿using EphingAutomation.Models.ConfigMgr;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace EphingAutomation.CM.StatusMessageProcessorService.Repository
{
    public class ProcessStatusMessage : IProcessStatusMessage
    {
        public Task StartProcessingAsync(StatusMessage message)
        {
            var task = Task.Run(() => StartProcessing(message));
            return task;
        }

        private void StartProcessing(StatusMessage message)
        {
            Log.Information("Processing status message {@message}", message);
        }
    }
}
