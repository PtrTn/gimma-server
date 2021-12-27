using Gimma.Models;
using Gimma.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace Gimma.Hubs
{
    public class GameHub : Hub
    {
        private readonly RandomStringRepository _randomStringRepository;
        private List<Game> _games = new();
        
        public GameHub(RandomStringRepository randomStringRepository)
        {
            _randomStringRepository = randomStringRepository;
        }

        public async Task CreateGame(string userName)
        {
            var host = new Player(userName, Context.ConnectionId);
            var gameId = _randomStringRepository.GenerateRandomString(4);
            var game = new Game(gameId, host);
            
            _games.Add(game);
            
            await Clients.Caller.SendAsync("GameCreated", "my-game-id");
        }
        
        public async Task JoinGame(string userName, string gameId)
        {
            var player = new Player(userName, Context.ConnectionId);
            var game = _games.FirstOrDefault(o => o._gameId == gameId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }

            game.Join(player);
            
            await Clients.Caller.SendAsync("GameJoined");
        }
        
        public async Task StartGame()
        {
            var game = _games.FirstOrDefault(g => g._host._connectionId == Context.ConnectionId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }
            
            await Clients.Clients(game._players.Select(o => o._connectionId)).SendAsync("GameStarted");
        }
    }
}