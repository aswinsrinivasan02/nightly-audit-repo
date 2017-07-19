using System;

namespace SG.NightlyAudit.DTO
{
    public enum SchedulingTypes
    {
        OneTime,
        Daily,
        Weekly,
        Monthly
    }

    public class ScheduleObject
    {
        public SchedulingTypes ScheduleType { get; set; }

        public DateTime StartDateTime { get; set; }

        public int ReoccurEveryX { get; set; }

        public int SelectedDays { get; set; }

        public int SelectedMonths { get; set; }

        public int SelectedOnWeek { get; set; }

        public int SelectedDates { get; set; }

        public Int64 JobId { get; set; }
    }
}
