using Gimma.CommandHandlers;
using Gimma.Commands;
using Gimma.RequestDtos;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using SignalRSwaggerGen.Enums;

namespace Gimma.Hubs
{
    [SignalRHub(path: "/game", autoDiscover: AutoDiscover.MethodsAndArgs)]
    public class GameHub : Hub<IGameHub>
    {
        private readonly CreateGameCommandHandler _createGameCommandHandler;
        private readonly JoinGameCommandHandler _joinGameCommandHandler;
        private readonly StartGameCommandHandler _startGameCommandHandler;
        private readonly SubmitAnswerCommandHandler _submitAnswerCommandHandler;

        public GameHub(
            CreateGameCommandHandler createGameCommandHandler,
            JoinGameCommandHandler joinGameCommandHandler,
            StartGameCommandHandler startGameCommandHandler,
            SubmitAnswerCommandHandler submitAnswerCommandHandler
        ) {
            _createGameCommandHandler = createGameCommandHandler;
            _joinGameCommandHandler = joinGameCommandHandler;
            _startGameCommandHandler = startGameCommandHandler;
            _submitAnswerCommandHandler = submitAnswerCommandHandler;
        }

        public async Task CreateGame(CreateGameRequest request)
        {
            if (request.UserName == null)
            {
                throw new Exception("Invalid request");
            }

            var command = new CreateGameCommand(request.UserName, Context.ConnectionId);
            await _createGameCommandHandler.Handle(command);
        }
        
        public async Task JoinGame(JoinGameRequest request)
        {
            if (request.UserName == null || request.GameId == null)
            {
                throw new Exception("Invalid request");
            }
            
            var command = new JoinGameCommand(request.UserName, request.GameId, Context.ConnectionId);
            await _joinGameCommandHandler.Handle(command);
        }
        
        public async Task StartGame(StartGameRequest request)
        {
            var command = new StartGameCommand(Context.ConnectionId);
            await _startGameCommandHandler.Handle(command);
        }
        
        public async Task SubmitAnswer(SubmitAnswerRequest request)
        {
            var command = new SubmitAnswerCommand(request.ImageId, Context.ConnectionId);
            await _submitAnswerCommandHandler.Handle(command);
        }
    }
}