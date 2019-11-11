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
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace EphingAutomation.CM.StatusMessageProcessorService
{
    public class Worker : BackgroundService
    {
        private List<Task> _workerTasks;
        private NamedPipeServerStream _pipeServer;
        IProcessStatusMessage _processStatusMessage;
        IAsyncResult _beginWait;
        DateTime startedBeginWait;
        public Worker(IProcessStatusMessage processStatusMessage)
        {
            _processStatusMessage = processStatusMessage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Starting background worker");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "EphingAdminStatusMessageQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    var consumer = new EventingBasicConsumer(channel);

                }
            }
            /*
            _pipeServer = new NamedPipeServerStream("EphingAdmin.CM.StatusMessages", PipeDirection.InOut);
            _workerTasks = new List<Task>();
            _beginWait = _pipeServer.BeginWaitForConnection(new AsyncCallback(WaitForConnectionCallBack), _pipeServer);
            startedBeginWait = DateTime.UtcNow;
            while (!stoppingToken.IsCancellationRequested)
            {
                Thread.Sleep(100);
                if (startedBeginWait < DateTime.UtcNow.AddMinutes(-10))
                {
                    await Task.WhenAll(_workerTasks.ToArray());
                    break;
                }
            }
            _pipeServer.Dispose();
            */
        }
        private void WaitForConnectionCallBack(IAsyncResult result)
        {

            var statusMessage = Serializer.Deserialize<StatusMessage>((NamedPipeServerStream)result.AsyncState);

            if (statusMessage != null)
            {
                Log.Information("Status message is {@statusMessage}", statusMessage);
            }
            _pipeServer.Close();
            _pipeServer = new NamedPipeServerStream("EphingAdmin.CM.StatusMessages", PipeDirection.InOut);
            startedBeginWait = DateTime.UtcNow;
            _beginWait = _pipeServer.BeginWaitForConnection(new AsyncCallback(WaitForConnectionCallBack), _pipeServer);
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
