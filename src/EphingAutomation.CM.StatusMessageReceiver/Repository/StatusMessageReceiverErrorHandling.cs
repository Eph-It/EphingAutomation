using EphingAutomation.Models.ConfigMgr;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EphingAutomation.CM.StatusMessageReceiver.Repository
{
    public class StatusMessageReceiverErrorHandling : IStatusMessageReceiverErrorHandling
    {
        IServiceRepository _serviceRepo;
        public StatusMessageReceiverErrorHandling()
        {
            _serviceRepo = new ServiceRepository();
        }
        private string ErrorFileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ToReprocess");
        public void SaveErrors(StatusMessage smObject)
        {
            System.IO.Directory.CreateDirectory(ErrorFileDirectory);
            using(var file = File.Create(Path.Combine(ErrorFileDirectory, $"{smObject.RequestId.ToString()}.bin")))
            {
                Serializer.Serialize(file, smObject);
            }
        }
        public void ProcessPreviousErrors()
        {
            System.IO.Directory.CreateDirectory(ErrorFileDirectory);
            DirectoryInfo dirInfo = new DirectoryInfo(ErrorFileDirectory);
            FileInfo[] Files = dirInfo.GetFiles("*.bin");
            foreach (FileInfo file in Files)
            {
                using (var fileObject = File.OpenRead(file.FullName))
                {
                    var statusMessageObject = Serializer.Deserialize<StatusMessage>(fileObject);
                    _serviceRepo.SendArgs(statusMessageObject);
                }
                File.Delete(file.FullName);
            }
        }
    }
}
