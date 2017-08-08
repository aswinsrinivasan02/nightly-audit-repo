using BallyTech.Infrastructure.Types;
using System;
using System.Collections.Generic;

namespace SG.NightlyAudit.Domain
{
    [Serializable]
    public class Job : Persistable, IAggregate<Int64>
    {
        public Int64 JobId { get; set; }

        public Int32 AuditId { get; set; }

        public String CronExpression { get; set; }

        public String ParametersChosen { get; set; }

        public DateTime StartDateTime { get; set; }

        public Boolean IsEnabled { get; set; }

        public JobSchedule JobSchedule { get; set; }

        public long Id
        {
            get { return JobId; }
        }
    }

    public class JobSchedule
    {
        public Int64 JobId { get; set; }

        public Int32 ScheduleType { get; set; }

        public DateTime StartDateTime { get; set; }

        public int ReoccurEveryX { get; set; }

        public int ReoccurEveryXMonths { get; set; }

        public int SelectedWeek { get; set; }

        public List<int> SelectedDates { get; set; }
    }
}
