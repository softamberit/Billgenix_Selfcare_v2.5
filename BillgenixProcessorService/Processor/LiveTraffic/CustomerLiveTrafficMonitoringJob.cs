using BillgenixProcessorService.ApiIntegration;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using System.Text.Json;

namespace BillgenixProcessorService.Processor.LiveTraffic;

public sealed class CustomerLiveTrafficMonitoringJob : IJob
{
    BillgenixRadiusClient _client;
    ILogger<CustomerLiveTrafficMonitoringJob> _logger;
    public IHubContext<CustomerTrafficHub> _trafficHub { get; }
    public int dataId = 0;
    public CustomerLiveTrafficMonitoringJob(BillgenixRadiusClient client,
        ILogger<CustomerLiveTrafficMonitoringJob> logger, IHubContext<CustomerTrafficHub> trafficHub)
    {

        _logger = logger;
        _trafficHub = trafficHub;
        _client = client;
    }
    public async Task Execute(IJobExecutionContext context)
    {

        _logger.LogInformation("User Live Traffic Monitoring Job started at {Time}", DateTime.Now);

        string cid = "74039";
        //  _mkService.get
        //var username = Context.GetHttpContext()?.Request.Query["username"].ToString() ?? "Anonymous";

        for (var i = 0; i < 10; i++)
        {
            try
            {

                var requests =  _client.GetTrafficData(cid);

                await _trafficHub.Clients.All.SendAsync("traffics", JsonSerializer.Serialize(requests));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing traffic request for user {CustomerID}", cid);
            }
        }

        await Task.Delay(1000);

    }

    //public async Task ProcessTrafficAsync(DailyRouterLog router, CustomerTrafficRequestDto request)
    //{

    //    //var traffics = await _mkService.GetLiveTraffic(new RouterConfig { Host = router.RouterIP, UserName = router.UserName, Password = router.Password, ApiPort = router.ApiPort }, request.MkUser);


    //    using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
    //    {
    //        connection.ReceiveTimeout = 60000;
    //        connection.SendTimeout = 60000;

    //        IEnumerable<ITikReSentence> response = null;
    //        connection.Open(router.RouterIP, router.ApiPort, router.UserName, router.Password);
    //        var seconds = request.TrafficDurationInMenutes * 60; // Convert minutes to seconds
    //        for (int i = 0; i < seconds; i++)
    //        {

    //            var traffic = await connection.LoadTraffic(request.MkUser);

    //            await _trafficHub.Clients.All.SendAsync("traffics", JsonSerializer.Serialize(traffic));
    //            await Task.Delay(1000);
    //        }


    //        // Interlocked.Increment(ref dataId);

    //        //if (dataId > seconds)
    //        //{
    //        request.ProcessStatus = 2;
    //        await _repoProcessor.UpdateTrafficRequestStatus(request);


    //        //}
    //        //if (dataId > 1)
    //        //{

    //        //}


    //    }
    //}
}

