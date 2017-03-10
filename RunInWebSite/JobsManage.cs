using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunInWebSite
{
    public class JobsManage
    {
 
        public static IScheduler sche = null;
        static JobsManage()
        {
            sche = new StdSchedulerFactory().GetScheduler();
            sche.Start();
        }


        internal static void Sche()
        {
            sche.Clear();
            LoadJobs().ForEach(i =>
            {
                if (i.Enable == "1")
                {
                    JobDataMap map = new JobDataMap();
                    map.Add("jobname", i);
                    IJobDetail job = JobBuilder.Create<ReportJob>()
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
        internal static List<ReportJob> LoadJobs() {
            return new List<ReportJob>() {
                new ReportJob() {
                    Enable="1",
                    Cron="*/5 * * * * ?",
                    Name="A"
                   
                },
               new ReportJob() {
                    Enable="1",
                    Cron="*/3 * * * * ?",
                                    Name="B"
                },
                new ReportJob() {
                    Enable="1",
                    Cron="*/7 * * * * ?",
                        Name="C"
                }
            };
        }
    }
}