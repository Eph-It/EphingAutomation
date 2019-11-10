using ProtoBuf;
using System;

namespace EphingAutomation.Models.ConfigMgr
{
    [ProtoContract]
    public class StatusMessage
    {
        [ProtoMember(1)]
        public string[] Arguments { get; set; }
        [ProtoMember(2)]
        public Guid RequestId { get; set; }
    }
}
