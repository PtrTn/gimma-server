using Microsoft.AspNetCore.SignalR;

namespace Gimma.Hubs;

public interface IGameHub
{
    Task CreateGame(string userName);
    Task JoinGame(string userName, string gameId);
    Task StartGame();
    Task OnConnectedAsync();
    Task OnDisconnectedAsync(Exception? exception);
    void Dispose();
    IHubCallerClients Clients { get; set; }
    HubCallerContext Context { get; set; }
    IGroupManager Groups { get; set; }
}