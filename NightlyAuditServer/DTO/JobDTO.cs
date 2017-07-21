using System;

namespace SG.NightlyAudit.DTO
{
    public class JobDTO
    {
        public Int64 JobId { get; set; }

        public Int32 AuditId { get; set; }

        public ScheduleObject ScheduleObject { get; set; }
    }
}
