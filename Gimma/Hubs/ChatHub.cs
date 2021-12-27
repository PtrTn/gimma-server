using Gimma.Models;
using Microsoft.AspNetCore.SignalR;

namespace Gimma.Hubs
{
    public class ChatHub : Hub
    {
        private List<Player> _playersInGame = new List<Player>();

        public async Task CreateGame(string userName)
        {
            _playersInGame.Add(new Player(userName, Context.ConnectionId, true));
            await Clients.Caller.SendAsync("GameCreated", "my-game-id");
        }
        
        public async Task JoinGame(string userName, string gameId)
        {
            _playersInGame.Add(new Player(userName, Context.ConnectionId, false));
            await Clients.Caller.SendAsync("GameJoined");
        }
        
        public async Task StartGame()
        {
            Clients.Clients(_playersInGame.Select(o => o._connectionId));
            await Clients.All.SendAsync("GameStarted");
        }
    }
}