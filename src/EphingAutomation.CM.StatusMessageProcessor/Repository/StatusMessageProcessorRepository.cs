using EphingAutomation.CM.StatusMessageDB;
using EphingAutomation.CM.StatusMessageDB.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageProcessor.Repository
{
    public class StatusMessageProcessorRepository
    {
        IStatusMessageRepository _smRepo;
        public StatusMessageProcessorRepository(StatusMessageDBContext dbContext)
        {
            _smRepo = new StatusMessageRepository(dbContext);
        }
        public void Start()
        {
            DateTime LastProcessed = DateTime.UtcNow;
            while(LastProcessed < DateTime.UtcNow.AddMinutes(5))
            {
                var nextSM = _smRepo.GetNext();
                if(nextSM != null)
                {
                    LastProcessed = DateTime.UtcNow;

                }
            }
        }
    }
}
