using Quartz.Impl;
using Quartz;

namespace TrafficProcessorService.ScheduleJob
{
    public class QuartzScheduleJob
    {
        public static async Task<IScheduler> GetSchedulers()
        {
           // var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler(); ;

            //scheduler.GetJobKeys()

            //var job = JobBuilder.Create<CustomerLiveTrafficMonitoringJob>()
            //    .WithIdentity("UserLiveTrafficMonitoringJob", "group 1")
            //    .Build();

            //var trigger = TriggerBuilder.Create()
            //    .WithIdentity("UserLiveTrafficMonitoring_trigger1", "group 1")
            //    .ForJob(job)
            //    .StartNow()
            //    .WithCronSchedule("0 /1 * ? * *")
            //    .Build();

            //await scheduler.ScheduleJob(job, trigger);

            //await scheduler.Start();

            return scheduler;
        }
    }
}
