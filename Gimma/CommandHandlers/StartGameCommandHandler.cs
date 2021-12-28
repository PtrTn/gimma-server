using Gimma.Commands;
using Gimma.Dispatchers;
using Gimma.Repositories;
using Gimma.ResponseDtos;

namespace Gimma.CommandHandlers;

public class StartGameCommandHandler
{
    private readonly GameRepository _gameRepository;
    private readonly EventDispatcher _eventDispatcher;

    public StartGameCommandHandler(GameRepository gameRepository, EventDispatcher eventDispatcher)
    {
        _gameRepository = gameRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(StartGameCommand command)
    {
        var game = _gameRepository.FetchByHostConnectionId(command.HostConnectionId);
        
        await _eventDispatcher.Dispatch(
            new GameStartedResponse(game._players.Select(o => o._connectionId).ToList())
        );
    }
}