namespace Gimma.Models;

public class Game
{
    public readonly string GameId;
    public readonly Player Host;
    public readonly List<Player> Players = new();

    public Game(string gameId, Player host)
    {
        GameId = gameId;
        Host = host;
        Players.Add(host);
    }

    public void Join(Player player)
    {
        Players.Add(player);
    }
}