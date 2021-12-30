using Gimma.ResponseDtos;

namespace Gimma.Models;

public class Round
{
    private readonly string _prompt;
    private readonly List<string> _playerConnectionIds;
    private List<Submission> _submissions = new();

    public Round(string prompt, List<string> playerConnectionIds)
    {
        _prompt = prompt;
        _playerConnectionIds = playerConnectionIds;
    }

    public void SubmitAnswer(Submission submission)
    {
        if (_submissions.Select(answer => answer.connectionId).Contains(submission.connectionId))
        {
            throw new Exception("A player can only submit once");
        }
        _submissions.Add(submission);
    }

    public bool IsComplete()
    {
        var playersWhoHaveSubmitted = _submissions.Select(submission => submission.connectionId).ToList();
        
        return _playerConnectionIds.All(playersWhoHaveSubmitted.Contains);
    }

    public string GetPrompt()
    {
        return _prompt;
    }

    public List<Submission> GetSubmissions()
    {
        return _submissions;
    }
}