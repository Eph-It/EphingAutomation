using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EphingAutomation.Models
{
    public class EAScriptBlock
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public int Created_By { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
        public int Last_Modified_By { get; set; }
        public string Code { get; set; }
    }
}
