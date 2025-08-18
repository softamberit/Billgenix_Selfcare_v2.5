using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace BillgenixProcessorService.Processor.LiveTraffic;

public class CustomerTrafficHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> ActiveUsers = new();
    public async override Task OnConnectedAsync()
    {
        var cid = Context.GetHttpContext()?.Request.Query["cid"].ToString() ?? "Anonymous";
        ActiveUsers[Context.ConnectionId] = cid;
        var response = new { connectionId = Context.ConnectionId, cid = cid };
        await Clients.Client(Context.ConnectionId).SendAsync("UserConnected", JsonConvert.SerializeObject(response));
        // await UpdateActiveUsers(Context.ConnectionId, cid);
        await base.OnConnectedAsync();
    }
    // Notify all users when a user leaves
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (ActiveUsers.TryRemove(Context.ConnectionId, out var cid))
        {
            await Clients.Client(Context.ConnectionId).SendAsync("UserDisconnected", cid);
            //await UpdateActiveUsers();
        }

        await base.OnDisconnectedAsync(exception);
    }
    // Send a message to all users
    //public async Task SendMessageToAll(string user, string message)
    //{
    //    await Clients.All.SendAsync("ReceiveMessage", user, message);
    //}

    // Send a private message to a specific user
    public async Task SendPrivateMessage(string sender, string receiver, string message)
    {
        var receiverConnectionId = ActiveUsers.FirstOrDefault(x => x.Value == receiver).Key;
        if (receiverConnectionId != null)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceivePrivateMessage", sender, message);
        }
    }
    // Update the list of active users for all clients
    private async Task UpdateActiveUsers(string connectionId, string customerId)
    {


        //var activeUsers = ActiveUsers.Values.Distinct().ToList();
        //await Clients.All.SendAsync("UpdateActiveUsers", activeUsers);
    }
}

