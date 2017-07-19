using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using System.Threading;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            ISchedulerFactory scheduleFactory = new StdSchedulerFactory();
            IJobDetail jobDetail = JobBuilder.Create<SayHellp>().WithIdentity("name", "group").Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().UsingJobData("Aswin", true).WithSimpleSchedule(s => s.WithIntervalInSeconds(5).WithRepeatCount(5)).Build();


            IScheduler scheduler = scheduleFactory.GetScheduler();
            scheduler.Start();

            scheduler.ScheduleJob(jobDetail, trigger);

            Thread.Sleep(TimeSpan.FromSeconds(10));
            scheduler.PauseJob(new JobKey("name", "group"));
            Thread.Sleep(TimeSpan.FromSeconds(8));
            scheduler.ResumeJob(new JobKey("name", "group"));
            Thread.Sleep(TimeSpan.FromMinutes(10));
            scheduler.Shutdown();
        }
    }

    [DisallowConcurrentExecution]
    public class SayHellp : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hi " +DateTime.Now.ToString());
        }
    }

}
