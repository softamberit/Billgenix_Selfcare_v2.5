using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace BillgenixProcessorService.Processor.LiveTraffic;

public class CustomerTrafficHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> ActiveUsers = new();
    public async override Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext()?.Request.Query["username"].ToString() ?? "Anonymous";
        ActiveUsers[Context.ConnectionId] = username;

        await Clients.All.SendAsync("UserConnected", username);
        await UpdateActiveUsers();
        await base.OnConnectedAsync();
    }
    // Notify all users when a user leaves
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (ActiveUsers.TryRemove(Context.ConnectionId, out var username))
        {
            await Clients.All.SendAsync("UserDisconnected", username);
            await UpdateActiveUsers();
        }

        await base.OnDisconnectedAsync(exception);
    }
    // Send a message to all users
    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

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
    private async Task UpdateActiveUsers()
    {
        var activeUsers = ActiveUsers.Values.Distinct().ToList();
        await Clients.All.SendAsync("UpdateActiveUsers", activeUsers);
    }
}

