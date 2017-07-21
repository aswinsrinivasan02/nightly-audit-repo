using System;
using System.Runtime.Serialization;

namespace SG.NightlyAudit.DTO
{
    public enum SchedulingTypes
    {
        OneTime,
        Daily,
        Weekly,
        Monthly
    }

    [DataContract]
    public class ScheduleObject
    {
        [DataMember]
        public SchedulingTypes ScheduleType { get; set; }

        [DataMember]
        public DateTime StartDateTime { get; set; }

        [DataMember]
        public int ReoccurEveryX { get; set; }

        [DataMember]
        public int SelectedDays { get; set; }

        [DataMember]
        public int SelectedMonths { get; set; }

        [DataMember]
        public int SelectedOnWeek { get; set; }

        [DataMember]
        public int SelectedDates { get; set; }

        [DataMember]
        public Int64 AuditId { get; set; }
    }
}
