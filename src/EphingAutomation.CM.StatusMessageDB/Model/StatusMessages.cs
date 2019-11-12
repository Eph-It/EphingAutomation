using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageDB.Model
{
    public class StatusMessages
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public DateTime Recorded { get; set; }
        public string InsString1 { get; set; }
        public string InsString2 { get; set; }
        public string InsString3 { get; set; }
        public string InsString4 { get; set; }
        public string InsString5 { get; set; }
        public string InsString6 { get; set; }
        public string InsString7 { get; set; }
        public string InsString8 { get; set; }
        public string InsString9 { get; set; }
        public string InsString10 { get; set; }
        public DateTime? Started { get; set; }
    }
}
