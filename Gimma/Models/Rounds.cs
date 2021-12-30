using System.Collections.ObjectModel;

namespace Gimma.Models;

public class Rounds : Collection<Round>
{
    private readonly List<Round> _rounds;
    private int _currentRound = 1;

    public Rounds(List<Round> rounds)
    {
        _rounds = rounds;
    }

    public Round GetCurrentRound()
    {
        return _rounds.ElementAt(_currentRound - 1);
    }

    public void StartNextRound()
    {
        if (_currentRound >= _rounds.Count)
        {
            throw new Exception("Last round has been reached");
        }
        
        _currentRound++;
    }
}