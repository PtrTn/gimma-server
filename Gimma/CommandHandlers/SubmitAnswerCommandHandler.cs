using Gimma.Commands;
using Gimma.Dispatchers;
using Gimma.Models;
using Gimma.Repositories;
using Gimma.ResponseDtos;

namespace Gimma.CommandHandlers;

public class SubmitAnswerCommandHandler
{
    private readonly GameRepository _gameRepository;
    private readonly EventDispatcher _eventDispatcher;

    public SubmitAnswerCommandHandler(GameRepository gameRepository, EventDispatcher eventDispatcher)
    {
        _gameRepository = gameRepository;
        _eventDispatcher = eventDispatcher;
    }
    
    public async Task Handle(SubmitAnswerCommand command)
    {
        var game = _gameRepository.FetchByPlayerConnectionId(command.PlayerConnectionId);
        var submission = new Submission(command.ImageId, command.PlayerConnectionId);
        var currentRound = game.GetRounds().GetCurrentRound();
        currentRound.SubmitAnswer(submission);

        if (currentRound.IsComplete())
        {
            await _eventDispatcher.Dispatch(
                new RoundCompletedResponse(currentRound.GetSubmissions(), game.GetPlayerConnectionIds())
            );
        }
    }
}