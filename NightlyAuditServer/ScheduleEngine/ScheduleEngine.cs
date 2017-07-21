using SG.NightlyAudit.DTO;
using Quartz;
using Quartz.Impl;
using System;
using ScheduleEngine.Utilities;

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

        public void AddJobForSchedule(ScheduleObject scheduleObject)
        {
            IJobDetail jobDetail = JobBuilder.Create<Job>().WithIdentity(scheduleObject.AuditId.ToString())
                                    .UsingJobData("JobId", scheduleObject.AuditId).Build();
            
            this.scheduler.DeleteJob(new JobKey(scheduleObject.AuditId.ToString()));

            string cronExpression = CronExpressionGenerator.GenerateCronExpression(scheduleObject);

            ITrigger trigger = TriggerBuilder.Create().StartAt(new DateTimeOffset(scheduleObject.StartDateTime)).WithCronSchedule(cronExpression).Build();
            scheduler.ScheduleJob(jobDetail, trigger);
        }
    }

    public class Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
