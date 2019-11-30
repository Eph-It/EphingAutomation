using EphingAutomation.CM.StatusMessageDB;
using EphingAutomation.CM.StatusMessageDB.Model;
using EphingAutomation.CM.StatusMessageDB.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EphingAutomation.CM.StatusMessageProcessor.Repository
{
    public class StatusMessageProcessorRepository
    {
        IStatusMessageRepository _smRepo;
        IJobRepository _jobRepository;
        List<Task> _backgroundJobs = new List<Task>();
        public StatusMessageProcessorRepository(StatusMessageDBContext dbContext)
        {
            _smRepo = new StatusMessageRepository(dbContext);
            _jobRepository = new JobRepository(dbContext);
        }
        private void BackgroundProcess(StatusMessage sm)
        {
            var smJobRepo = new StatusMessageJobRepository(sm);
            var backgroundJob = Task.Run(() => smJobRepo.Start());
            
            _backgroundJobs.Add(backgroundJob);
        }
        private void RemoveCompletedTasks()
        {
            for (int i = 0; i < _backgroundJobs.Count; i++)
            {
                var t = _backgroundJobs[i];
                if (t.IsCompleted)
                {
                    _backgroundJobs.Remove(t);
                }
            }
        }
        public void Start()
        {
            _jobRepository.New();
            DateTime LastProcessed = DateTime.UtcNow;
            while (LastProcessed < DateTime.UtcNow.AddMinutes(5))
            {
                if(_backgroundJobs.Count > 0)
                {
                    RemoveCompletedTasks();
                }
                var nextSM = _smRepo.GetNext();
                if (nextSM != null)
                {
                    LastProcessed = DateTime.UtcNow;
                    BackgroundProcess(nextSM);
                }
                Thread.Sleep(100);
            }
            _jobRepository.EndJob();
            var lastSM = _smRepo.GetNext();
            if (lastSM != null)
            {
                BackgroundProcess(lastSM);
            }
            Task.WaitAll(_backgroundJobs.ToArray());
        }
    }
    
}
