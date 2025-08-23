using BillgenixProcessorService.ApiIntegration;
using BillgenixProcessorService.Models;
using BillgenixProcessorService.Repositories;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using System.Security.Cryptography;
using System.Text.Json;

namespace BillgenixProcessorService.Processor.LiveTraffic;

public sealed class CustomerLiveTrafficMonitoringJob : IJob
{
    BillgenixRadiusClient _client;
    ILogger<CustomerLiveTrafficMonitoringJob> _logger;
    private IHubContext<CustomerTrafficHub> _trafficHub { get; }
    private int dataId = 0;
    private IBillgenixRepository _repo;
    public CustomerLiveTrafficMonitoringJob(BillgenixRadiusClient client,
        ILogger<CustomerLiveTrafficMonitoringJob> logger, IHubContext<CustomerTrafficHub> trafficHub, IBillgenixRepository repo)
    {

        _logger = logger;
        _trafficHub = trafficHub;
        _client = client;
        _repo = repo;
    }

    //_trafficHub.Clients.Client(request.ConnectionId)


    public async Task TrafficStartAsync(string connectionId, string message)
    {
        //  await _trafficHub.Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        var requests = await GetPendingRequests(connectionId);
        BackgroundJob.Enqueue(() => ProcessTrafficAsync(requests));

    }

    public async Task Execute(IJobExecutionContext context)
    {

        _logger.LogInformation("User Live Traffic Monitoring Job started at {Time}", DateTime.Now);


        var requests = await GetPendingRequests();


        // string cid = "74039";
        //  _mkService.get
        //var username = Context.GetHttpContext()?.Request.Query["username"].ToString() ?? "Anonymous";

        foreach (var request in requests)
        {
            try
            {
                if (request.ProcessStatus == 0)
                {
                    BackgroundJob.Enqueue(() => ProcessTrafficAsync(request));

                    _logger.LogInformation("Processing traffic request for user {CustomerID}", request.CustomerID);

                    //var traffic = _client.GetTrafficData(request.CustomerID);
                    //await _trafficHub.Clients.All.SendAsync("traffics", JsonSerializer.Serialize(traffic));
                    request.ProcessStatus = 1;
                    await _repo.UpdateTrafficRequestStatus(new UpdateTrafficRequestDto { Id = request.Id, ProcessStatus = request.ProcessStatus });
                }
                else
                {
                    _logger.LogInformation("Skipping already processed traffic request for user {CustomerID}", request.CustomerID);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing traffic request for user {CustomerID}", request.CustomerID);
            }
        }

        //for (var i = 0; i < 10; i++)
        //{
        //    try
        //    {

        //        var traffic = _client.GetTrafficData(cid);

        //        await _trafficHub.Clients.All.SendAsync("traffics", JsonSerializer.Serialize(traffic));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error processing traffic request for user {CustomerID}", cid);
        //    }
        //}

        await Task.Delay(1000);

    }

    public async Task ProcessTrafficAsync(CustomerTrafficRequestDto request)
    {
        //
        int seconds = request.TrafficDurationInMenutes * 60;
        float totalRx = 0;
        float totalTx = 0;

        for (int i = 0; i < seconds; i++)
        {
            // Simulate traffic data retrieval
            var traffic = _client.GetTrafficData(request.CustomerID);
            totalRx += traffic.Rx;
            totalTx += traffic.Tx;
            //traffic.AvgRx = (int)(totalRx / (i + 1));
            //traffic.AvgTx = (int)(totalTx / (i + 1));
            await _trafficHub.Clients.Client(request.ConnectionId).SendAsync("traffics", JsonSerializer.Serialize(traffic));

            await Task.Delay(1000); // Simulate delay for each minute of traffic data

        }


        int avgRx = (int)(totalRx / seconds);
        int avgTx = (int)(totalTx / seconds);


        request.ProcessStatus = 2;
        await _repo.UpdateTrafficRequestStatus(new UpdateTrafficRequestDto { Id = request.Id, ProcessStatus = request.ProcessStatus, AvgRx = avgRx, AvgTx = avgTx });

        await _trafficHub.Clients.Client(request.ConnectionId).SendAsync("traffic_finished", JsonSerializer.Serialize(new CustomerTraffic { AvgRx = avgRx, AvgTx = avgTx }));

    }

    private async Task<IEnumerable<CustomerTrafficRequestDto>> GetPendingRequests()
    {

        return await _repo.GetPendingTrafficRequest();

    }
    private async Task<CustomerTrafficRequestDto> GetPendingRequests(string connectionId)
    {

        return await _repo.GetPendingTrafficRequest(connectionId);

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

