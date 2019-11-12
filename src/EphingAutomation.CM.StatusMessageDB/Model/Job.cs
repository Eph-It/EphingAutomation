using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageDB.Model
{
    public class Job
    {
        public int Id { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
        public int ProcessId { get; set; }
    }
}
