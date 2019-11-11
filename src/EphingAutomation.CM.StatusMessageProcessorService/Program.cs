using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EphingAutomation.CM.StatusMessageProcessorService.Repository;
using EphingAutomation.Logging;
using EphingAutomation.Models.ConfigMgr;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting.WindowsServices;
using ProtoBuf;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace EphingAutomation.CM.StatusMessageProcessorService
{
    public static class Program
    {
 
        public static void Main(string[] args)
        {
            var loggerConfig = new EALogging();
            loggerConfig.Configure("StatusMessageProcessorService");
            if (Environment.UserInteractive)
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                //lastProcessedMessage = DateTime.UtcNow;
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
                        consumer.Received += (model, ea) =>
                        {
                            StatusMessage statusMessage;
                            using (var stream = new MemoryStream(ea.Body))
                            {
                                statusMessage = Serializer.Deserialize<StatusMessage>(stream);
                            }
                            Log.Information("Status message is {@statusMessage}", statusMessage);
                            Console.WriteLine("test");
                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                            //lastProcessedMessage = DateTime.UtcNow;
                        };
                        channel.BasicConsume(queue: "EphingAdminStatusMessageQueue",
                                     autoAck: false,
                                     consumer: consumer);
                        //while (lastProcessedMessage > DateTime.UtcNow.AddMinutes(-10))
                        //{
                        //    Thread.Sleep(1000);
                        //}
                    }
                }
                var backgroundWorker = new Worker(new ProcessStatusMessage());
                backgroundWorker.StartAsync(new System.Threading.CancellationToken());
                Console.ReadLine();
                backgroundWorker.StopAsync(new System.Threading.CancellationToken());
                Console.ReadLine();
                backgroundWorker.Dispose();
            }
            else
            {
                // Service mode
                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IProcessStatusMessage, ProcessStatusMessage>();
                    services.AddHostedService<Worker>();
                });
    }
}
