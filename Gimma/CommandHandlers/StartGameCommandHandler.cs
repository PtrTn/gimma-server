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
    private readonly ImageRepository _imageRepository;
    private readonly EventDispatcher _eventDispatcher;

    public StartGameCommandHandler(
        GameRepository gameRepository,
        PromptRepository promptRepository,
        ImageRepository imageRepository,
        EventDispatcher eventDispatcher
    ) {
        _gameRepository = gameRepository;
        _promptRepository = promptRepository;
        _imageRepository = imageRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(StartGameCommand command)
    {
        var game = _gameRepository.FetchByHostConnectionId(command.HostConnectionId);
        var prompts = _promptRepository.GetPrompts(5);
        var rounds = prompts.Select(prompt => new Round(prompt, game.GetPlayerConnectionIds())).ToList();
        game.StartGame(new Rounds(rounds));
        
        await _eventDispatcher.Dispatch(
            new GameStartedResponse(game.GetPlayerConnectionIds())
        );
        
        await Task.Delay(3000).ContinueWith(t=> StartRound(game));
    }

    private async Task StartRound(Game game)
    {
        game.GetRounds().StartNextRound();
        
        var prompt = game.GetRounds().GetCurrentRound().GetPrompt();
        var images = _imageRepository.GetRandomImages(game.GetPlayerCount());
        var imagesPerPlayer = images.Chunk(6);

        for (int i = 0; i < game.GetPlayerConnectionIds().Count; i++)
        {
            await _eventDispatcher.Dispatch(
                new RoundStartedResponse(
                    prompt,
                    imagesPerPlayer.ElementAt(i).ToList(),
                    game.GetPlayerConnectionIds().ElementAt(i)
                )
            );
        }

    }
}