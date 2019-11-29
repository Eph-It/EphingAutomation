using System;

namespace EphingAutomation.CM.Models
{
    public class Collection
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string LimitingCollectionId { get; set; }
        public Collection LimitingCollection { get; set; }
        public string Type { get; set; }
        public string CollectionId { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime LastMembershipChange { get; set; }
        public bool UseIncrementalChanges { get; set; }
        public bool ScheduleFullUpdate { get; set; }

    }
}
