using Gimma.Commands;
using Gimma.Dispatchers;
using Gimma.Models;
using Gimma.Repositories;
using Gimma.ResponseDtos;

namespace Gimma.CommandHandlers;

public class StartGameCommandHandler
{
    private readonly GameRepository _gameRepository;
    private readonly PromptRepository _promptRepository;
    private readonly EventDispatcher _eventDispatcher;

    public StartGameCommandHandler(
        GameRepository gameRepository,
        PromptRepository promptRepository,
        EventDispatcher eventDispatcher
    ) {
        _gameRepository = gameRepository;
        _promptRepository = promptRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(StartGameCommand command)
    {
        var game = _gameRepository.FetchByHostConnectionId(command.HostConnectionId);
        var prompts = _promptRepository.GetPrompts(5);
        
        game.StartGame(prompts);
        
        await _eventDispatcher.Dispatch(
            new GameStartedResponse(game.GetPlayerConnectionIds())
        );
        
        await Task.Delay(3000).ContinueWith(t=> StartGame(game));
    }

    private async Task StartGame(Game game)
    {
        game.NextRound();
        
        var prompt = game.GetPromptForCurrentRound();
        
        await _eventDispatcher.Dispatch(
            new RoundStartedResponse(prompt, game.GetPlayerConnectionIds())
        );
    }
}