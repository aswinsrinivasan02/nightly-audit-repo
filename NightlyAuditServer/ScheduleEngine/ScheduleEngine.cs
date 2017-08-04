using Quartz;
using Quartz.Impl;
using System;
using SG.NightlyAudit.Domain;

namespace SG.NightlyAudit.ScheduleEngine
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
            if (scheduleObject.IsEnabled)
            {
                IJobDetail jobDetail = JobBuilder.Create<JobClass>().WithIdentity(scheduleObject.JobId.ToString())
                                        .UsingJobData("JobId", scheduleObject.JobId).Build();

                this.scheduler.DeleteJob(new JobKey(scheduleObject.JobId.ToString()));

                string cronExpression = scheduleObject.CronExpression;

                if (cronExpression != string.Empty)
                {
                    ITrigger trigger = TriggerBuilder.Create().StartAt(new DateTimeOffset(scheduleObject.StartDateTime)).WithCronSchedule(cronExpression).Build();
                    scheduler.ScheduleJob(jobDetail, trigger);
                }
                else
                {
                    //Simple Trigger for one time run.
                }
            }
            else
            {
                this.scheduler.DeleteJob(new JobKey(scheduleObject.JobId.ToString()));
            }
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
