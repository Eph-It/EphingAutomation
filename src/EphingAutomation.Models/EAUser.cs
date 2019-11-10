using System;

namespace EphingAutomation.Models
{
    public class EAUser
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int Created_By { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
        public DateTime Last_Modified { get; set; }
        public int Last_Modified_By { get; set; }
    }
}
