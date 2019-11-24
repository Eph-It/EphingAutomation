using EphingAutomation.CM.StatusMessageDB;
using EphingAutomation.CM.StatusMessageDB.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EphingAutomation.CM.StatusMessageProcessor.Repository
{
    public class StatusMessageProcessorRepository
    {
        IStatusMessageRepository _smRepo;
        IJobRepository _jobRepository;

        public StatusMessageProcessorRepository(StatusMessageDBContext dbContext)
        {
            _smRepo = new StatusMessageRepository(dbContext);
            _jobRepository = new JobRepository(dbContext);
        }
        public void Start()
        {
            _jobRepository.New();
            DateTime LastProcessed = DateTime.UtcNow;
            while(LastProcessed < DateTime.UtcNow.AddMinutes(5))
            {
                var nextSM = _smRepo.GetNext();
                if(nextSM != null)
                {
                    LastProcessed = DateTime.UtcNow;

                }
                Thread.Sleep(100);
            }
            _jobRepository.EndJob();
        }
    }
}
