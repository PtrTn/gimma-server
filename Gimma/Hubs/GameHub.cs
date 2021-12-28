using Gimma.Dispatchers;
using Gimma.Models;
using Gimma.Repositories;
using Gimma.ResponseDtos;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using SignalRSwaggerGen.Enums;

namespace Gimma.Hubs
{
    [SignalRHub(path: "/game", autoDiscover: AutoDiscover.MethodsAndArgs)]
    public class GameHub : Hub
    {
        private readonly EventDispatcher _eventDispatcher;
        private readonly RandomStringRepository _randomStringRepository;
        private List<Game> _games = new();
        
        public GameHub(EventDispatcher eventDispatcher, RandomStringRepository randomStringRepository)
        {
            _eventDispatcher = eventDispatcher;
            _randomStringRepository = randomStringRepository;
        }

        public async Task CreateGame(string userName)
        {
            var host = new Player(userName, Context.ConnectionId);
            var gameId = _randomStringRepository.GenerateRandomString(4);
            var game = new Game(gameId, host);
            
            _games.Add(game);

            await _eventDispatcher.Dispatch(
                new GameCreatedResponse(gameId, host._connectionId)
            );
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

            await _eventDispatcher.Dispatch(
                new GameJoinedResponse(player._connectionId)
            );
        }
        
        public async Task StartGame()
        {
            var game = _games.FirstOrDefault(g => g._host._connectionId == Context.ConnectionId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }
            
            await _eventDispatcher.Dispatch(
                new GameStartedResponse(game._players.Select(o => o._connectionId).ToList())
            );
        }
    }
}