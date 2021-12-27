namespace Gimma.Models;

public class Game
{
    public readonly string _gameId;
    public readonly Player _host;
    public readonly List<Player> _players = new();

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
}