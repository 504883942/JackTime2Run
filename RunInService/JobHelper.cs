using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunInService
{
   public  class JobHelper
    {
        public static List<Task_t> Tasks {
            get {
                TaskEntities db = new TaskEntities();
                return db.Task_t.ToList();
            }
        }
        public static IScheduler sche = null;
        static JobHelper()
        {
            sche = new StdSchedulerFactory().GetScheduler();
            sche.Start();
        }


        internal static void Sche()
        {
            sche.Clear();
            Tasks.ForEach(i =>
            {
                if (i.Enable == 1)
                {
                    JobDataMap map = new JobDataMap();
                    map.Add("task", i);
                    IJobDetail job = JobBuilder.Create<FullJob>()
                        .UsingJobData(map)
                        .WithIdentity(i.Name, i.Name)
                        .Build();

                    ICronTrigger tri = (ICronTrigger)TriggerBuilder.Create()
                        .StartNow()
                        .WithIdentity("tri_" + i.Name, "tri_" + i.Name)
                        .WithCronSchedule(i.Cron)
                        .Build();
                    sche.ScheduleJob(job, tri);
                }
            });
        }
    }
}
