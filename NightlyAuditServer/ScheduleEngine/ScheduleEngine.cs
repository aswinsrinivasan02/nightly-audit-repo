using Quartz;
using Quartz.Impl;
using System;
using SG.NightlyAudit.Domain;

namespace ScheduleEngine
{
    public class ScheduleEngine
    {
        private static ScheduleEngine _scheduleEngine;
        private IScheduler scheduler;

        public static ScheduleEngine Instance
        {
            get
            {
                if (_scheduleEngine == null)
                {
                    _scheduleEngine = new ScheduleEngine();
                }
                return _scheduleEngine;
            }

        }

        public ScheduleEngine()
        {
            ISchedulerFactory scheduleFactory = new StdSchedulerFactory();
            this.scheduler = scheduleFactory.GetScheduler();
            this.scheduler.Start();
        }

        public void AddJobForSchedule(Job scheduleObject)
        {
            IJobDetail jobDetail = JobBuilder.Create<JobClass>().WithIdentity(scheduleObject.AuditId.ToString())
                                    .UsingJobData("JobId", scheduleObject.AuditId).Build();

            this.scheduler.DeleteJob(new JobKey(scheduleObject.AuditId.ToString()));

            string cronExpression = scheduleObject.CronExpression;
            ITrigger trigger = TriggerBuilder.Create().StartAt(new DateTimeOffset(scheduleObject.StartDateTime)).WithCronSchedule(cronExpression).Build();
            scheduler.ScheduleJob(jobDetail, trigger);
        }
    }

    public class JobClass : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
