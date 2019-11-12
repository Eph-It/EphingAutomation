using EphingAutomation.CM.StatusMessageDB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageDB.Repository
{
    public interface IJobRepository
    {
        List<Job> GetActiveJobs();
        void New();
        void EndJob();
    }
}
