using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EphingAutomation.CM.StatusMessageProcessorService.Repository;
using EphingAutomation.Logging;
using EphingAutomation.Models.ConfigMgr;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProtoBuf;
using Serilog;

namespace EphingAutomation.CM.StatusMessageProcessorService
{
    public class Worker : BackgroundService
    {
        private List<Task> _workerTasks;
        private NamedPipeServerStream _pipeServer;
        IProcessStatusMessage _processStatusMessage;
        public Worker(IProcessStatusMessage processStatusMessage)
        {
            _processStatusMessage = processStatusMessage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            _pipeServer = new NamedPipeServerStream("EphingAdmin.CM.StatusMessages", PipeDirection.InOut);
            _workerTasks = new List<Task>();
            IAsyncResult beginWait = _pipeServer.BeginWaitForConnection(WaitForConnectionCallBack, null);
            DateTime startedBeginWait = DateTime.UtcNow;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (beginWait.IsCompleted)
                {
                    beginWait = _pipeServer.BeginWaitForConnection(WaitForConnectionCallBack, null);
                }
                Thread.Sleep(100);
                if(startedBeginWait < DateTime.UtcNow.AddMinutes(-10))
                {
                    _pipeServer.EndWaitForConnection(beginWait);
                    await Task.WhenAll(_workerTasks.ToArray());
                    break;
                }
            }
            _pipeServer.Dispose();
        }
        private void WaitForConnectionCallBack(IAsyncResult result)
        {
            Log.Information("Wait for connection called");
            var statusMessage = Serializer.Deserialize<StatusMessage>(_pipeServer);
            if(statusMessage != null)
            {

            }
        }
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var loggerConfig = new EALogging();
            loggerConfig.Configure("StatusMessageProcessorService");
            Log.Information("Starting service...");

            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it, this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }
            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public override void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
