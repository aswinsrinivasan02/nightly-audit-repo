using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SG.NightlyAudit.DTO
{
    public enum SchedulingTypes : int
    {
        OneTime = 0,
        Hours,
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

        //Every 2 Hours
        //Every 2 Days
        //Every {2} Day of X Month.
        //Every X Week 0 day of every Y Month.
        [DataMember]
        public int ReoccurEveryX { get; set; }

        //Every X day of 2 Months.
        //Every X Week Y day of every 2 Month.
        [DataMember]
        public int ReoccurEveryXMonths { get; set; }

        //Every 1st week of X day of every Y Months.
        [DataMember]
        public int SelectedWeek { get; set; }

        //Used for weekly. every sunday,monday, etc. Starts with Sunday = 0.
        [DataMember]
        public List<int> SelectedDates { get; set; }

        [DataMember]
        public Int64 JobId { get; set; }
    }
}
