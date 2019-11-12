using System;
using System.Collections.Generic;
using System.Text;
using EphingAutomation.CM.StatusMessageDB.Model;

namespace EphingAutomation.CM.StatusMessageDB.Repository
{
    public class JobRepository : IJobRepository
    {
        StatusMessageDBContext _db;
        public JobRepository(StatusMessageDBContext db)
        {
            _db = db;
        }

        public Job GetJob()
        {

        }
    }
}
