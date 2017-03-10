using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace RunInWebSite
{
    public class ReportJobScheduler
    {
        private static IScheduler scheduler
        {
            get
            {
                return StdSchedulerFactory.GetDefaultScheduler();
            }
        }
        public static IList<IJobExecutionContext> Current
        {
            get
            {
                return StdSchedulerFactory.GetDefaultScheduler().GetCurrentlyExecutingJobs();
            }
        }
        public static List<string> Tasklist = new List<string>();
        public static void AddTask(string key)
        {
            Tasklist.Add(key);
        }
        public static void RemoveTask(string key)
        {
            Tasklist.Remove(key);
        }
        public static List<IJobDetail> GetJobs()
        {
            List<IJobDetail> list = new List<IJobDetail>();
            Tasklist.ForEach(p => {
                list.Add(scheduler.GetJobDetail(new JobKey(p)));
            });
            return list;
        }
        public static void Start()
        {
            scheduler.Start();
        }
        public static void Run()
        {
            IJobDetail job = JobBuilder.Create<ReportJob>().Build();
            string taskid = Guid.NewGuid().ToString();
            Tasklist.Add(taskid);
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(taskid, "groupName")
                .WithSimpleSchedule(t =>
                t.WithIntervalInSeconds(1)
                    .RepeatForever())
                    .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}