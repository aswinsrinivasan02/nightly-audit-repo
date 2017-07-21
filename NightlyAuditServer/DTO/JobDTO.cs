using System;
using System.Runtime.Serialization;

namespace SG.NightlyAudit.DTO
{
    [DataContract]
    public class JobDTO
    {
        [DataMember]
        public Int64 JobId { get; set; }

        [DataMember]
        public Int32 AuditId { get; set; }

        [DataMember]
        public string ParametersChosen { get; set; }

        [DataMember]
        public ScheduleObject ScheduleObject { get; set; }

        [DataMember]
        public Boolean IsEnabled { get; set; }
    }
}
