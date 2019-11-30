using System;

namespace EphingAutomation.CM.Models
{
    public class v_Collections
    {
        public int CollectionID { get; set; }
        public string SiteID { get; set; }
        public string CollectionName { get; set; }
        public int Flags { get; set; }
        public string ResultTableName { get; set; }
        public string CollectionComment { get; set; }
        public string Schedule { get; set; }
        public int? SourceLocaleID { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public DateTime LastRefreshRequest { get; set; }
        public int CollectionType { get; set; }
        public string LimitToCollectionID { get; set; }
        public int IsReferenceCollection { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EvaluationStartTime { get; set; }
        public DateTime? LastRefreshTime { get; set; }
        public DateTime? LastIncrementalRefreshTime { get; set; }
        public DateTime? LastMemberChangeTime { get; set; }
        public int CurrentStatus { get; set; }
        public DateTime CurrentStatusTime { get; set; }
        public string LimitToCollectionName { get; set; }
        public byte[] ISVData { get; set; }
        public string ISVString { get; set; }
        public int CollectionVariablesCount { get; set; }
        public int ServiceWindowsCount { get; set; }
        public int PowerConfigsCount { get; set; }
        public int? RefreshType { get; set; }
        public int? MonitoringFlags { get; set; }
        public int IsBuiltIn { get; set; }
        public int? IncludeExcludeCollectionsCount { get; set; }
        public int MemberCount { get; set; }
        public int LocalMemberCount { get; set; }
        public string ResultClassName { get; set; }
        public int HasProvisionedMember { get; set; }
        public int? UseCluster { get; set; }
        public string ObjectPath { get; set; }
    }
}
