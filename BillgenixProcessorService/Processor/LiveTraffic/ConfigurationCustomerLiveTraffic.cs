using Microsoft.Extensions.Options;
using Quartz;
using TrafficProcessorService.Processor.LiveTraffic;

namespace TrafficProcessorService.Processor.LiveTraffic;
 public sealed class ConfigurationCustomerLiveTraffic()
    : IConfigureOptions<QuartzOptions>
{
    //private readonly RouterMonitoringOptions _outboxOptions = outboxOptions.CurrentValue;

    public void Configure(QuartzOptions options)
    {
       


        string jobName = "CustomerLiveTrafficMonitoringJob";
        //var startAt = DateTimeOffset.Parse(_outboxOptions.StartAt);
        options
            .AddJob<CustomerLiveTrafficMonitoringJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                     .StartAt(DateTimeOffset.UtcNow.AddSeconds(2)) // Start 1 minute from now
                    //.StartAt(startAt)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(10).RepeatForever()));


        //ITrigger trigger = TriggerBuilder.Create()
        //.WithIdentity("trigger1")
        //.StartAt(startTime)
        //.WithSimpleSchedule(x => x.RepeatForever().WithIntervalInHours(24))
        //.Build();
    }
}
