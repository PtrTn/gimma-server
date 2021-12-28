using Gimma.Commands;
using Gimma.Dispatchers;
using Gimma.Models;
using Gimma.Repositories;
using Gimma.ResponseDtos;

namespace Gimma.CommandHandlers;

public class JoinGameCommandHandler
{
    private readonly GameRepository _gameRepository;
    private readonly EventDispatcher _eventDispatcher;

    public JoinGameCommandHandler(GameRepository gameRepository, EventDispatcher eventDispatcher)
    {
        _gameRepository = gameRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(JoinGameCommand command)
    {
        var game = _gameRepository.FetchByGameId(command.GameId);
        var player = new Player(command.UserName, command.HostConnectionId);
        
        game.Join(player);

        await _eventDispatcher.Dispatch(
            new GameJoinedResponse(player._connectionId)
        );
    }
}