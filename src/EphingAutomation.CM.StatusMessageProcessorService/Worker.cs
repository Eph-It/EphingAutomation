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
        IHostEnvironment _environment;
        IAsyncResult _beginWait;
        public Worker(IProcessStatusMessage processStatusMessage, IHostEnvironment environment)
        {
            _processStatusMessage = processStatusMessage;
            _environment = environment;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Starting background worker");
            _pipeServer = new NamedPipeServerStream("EphingAdmin.CM.StatusMessages", PipeDirection.InOut);
            _workerTasks = new List<Task>();
            _beginWait = _pipeServer.BeginWaitForConnection(WaitForConnectionCallBack, null);
            DateTime startedBeginWait = DateTime.UtcNow;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_beginWait.IsCompleted)
                {
                    _beginWait = _pipeServer.BeginWaitForConnection(WaitForConnectionCallBack, _pipeServer);
                }
                Thread.Sleep(100);
                if(startedBeginWait < DateTime.UtcNow.AddMinutes(-10))
                {
                    _pipeServer.EndWaitForConnection(_beginWait);
                    await Task.WhenAll(_workerTasks.ToArray());
                    break;
                }
            }
            _pipeServer.Dispose();
        }
        private void WaitForConnectionCallBack(IAsyncResult result)
        {
            _pipeServer.EndWaitForConnection(result);
            var statusMessage = Serializer.Deserialize<StatusMessage>(_pipeServer);
            //_pipeServer.EndWaitForConnection(_beginWait);
            if(statusMessage != null)
            {
                Log.Information("Status message is {@statusMessage}", statusMessage);
            }
            
        }
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            
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
