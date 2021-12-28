using Gimma.Dispatchers;
using Gimma.Models;
using Gimma.Repositories;
using Gimma.RequestDtos;
using Gimma.ResponseDtos;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using SignalRSwaggerGen.Enums;

namespace Gimma.Hubs
{
    [SignalRHub(path: "/game", autoDiscover: AutoDiscover.MethodsAndArgs)]
    public class GameHub : Hub<IGameHub>
    {
        private readonly EventDispatcher _eventDispatcher;
        private readonly RandomStringRepository _randomStringRepository;
        private List<Game> _games = new();
        
        public GameHub(EventDispatcher eventDispatcher, RandomStringRepository randomStringRepository)
        {
            _eventDispatcher = eventDispatcher;
            _randomStringRepository = randomStringRepository;
        }

        public async Task CreateGame(CreateGameRequest request)
        {
            if (request.UserName == null)
            {
                throw new Exception("Invalid request");
            }
            
            var host = new Player(request.UserName, Context.ConnectionId);
            var gameId = _randomStringRepository.GenerateRandomString(4);
            var game = new Game(gameId, host);
            
            _games.Add(game);

            await _eventDispatcher.Dispatch(
                new GameCreatedResponse(gameId, host._connectionId)
            );
        }
        
        public async Task JoinGame(JoinGameRequest request)
        {
            if (request.UserName == null || request.GameId == null)
            {
                throw new Exception("Invalid request");
            }
            
            var player = new Player(request.UserName, Context.ConnectionId);
            var game = _games.FirstOrDefault(o => o._gameId == request.GameId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }

            game.Join(player);

            await _eventDispatcher.Dispatch(
                new GameJoinedResponse(player._connectionId)
            );
        }
        
        public async Task StartGame(StartGameRequest request)
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