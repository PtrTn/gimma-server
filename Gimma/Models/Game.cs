namespace Gimma.Models;

public class Game
{
    private readonly string _gameId;
    private readonly Player _host;
    private readonly List<Player> _players = new();
    private int _round;
    private List<string> _prompts;

    public Game(string gameId, Player host)
    {
        _gameId = gameId;
        _host = host;
        _players.Add(host);
    }

    public void Join(Player player)
    {
        _players.Add(player);
    }

    public void StartGame(List<string> prompts)
    {
        _prompts = prompts;
    }

    public void NextRound()
    {
        _round++;
    }

    public string GetPromptForCurrentRound()
    {
        return _prompts.ElementAt(_round);
    }

    public string GetGameId()
    {
        return _gameId;
    }

    public string GetHostConnectionId()
    {
        return _host._connectionId;
    }

    public List<string> GetPlayerConnectionIds()
    {
        return _players.Select(o => o._connectionId).ToList();
    }
}