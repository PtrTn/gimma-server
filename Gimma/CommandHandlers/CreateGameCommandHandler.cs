using Gimma.Commands;
using Gimma.Dispatchers;
using Gimma.Models;
using Gimma.Repositories;
using Gimma.ResponseDtos;

namespace Gimma.CommandHandlers;

public class CreateGameCommandHandler
{
    private readonly RandomStringRepository _randomStringRepository;
    private readonly GameRepository _gameRepository;
    private readonly EventDispatcher _eventDispatcher;

    public CreateGameCommandHandler(
        RandomStringRepository randomStringRepository,
        GameRepository gameRepository,
        EventDispatcher eventDispatcher
    ) {
        _randomStringRepository = randomStringRepository;
        _gameRepository = gameRepository;
        _eventDispatcher = eventDispatcher;
    }
    
    public async Task Handle(CreateGameCommand command)
    {
        var host = new Player(command.UserName, command.HostConnectionId);
        var gameId = _randomStringRepository.GenerateRandomString(4);
        var game = new Game(gameId, host);
            
        _gameRepository.Add(game);

        await _eventDispatcher.Dispatch(
            new GameCreatedResponse(gameId, host._connectionId)
        );
    }
}