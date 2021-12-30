using Gimma.Models;

namespace Gimma.Repositories;

public class GameRepository
{
    private List<Game> _games = new();

    public void Add(Game game)
    {
        _games.Add(game);
    }

    public Game FetchByGameId(string gameId)
    {
        var game = _games.FirstOrDefault(o => o.GetGameId() == gameId);
        if (game == null)
        {
            throw new Exception("Game not found");
        }

        return game;
    }

    public Game FetchByHostConnectionId(string hostConnectionId)
    {
        var game = _games.FirstOrDefault(o => o.GetHostConnectionId() == hostConnectionId);
        if (game == null)
        {
            throw new Exception("Game not found");
        }

        return game;
    }

    public Game FetchByPlayerConnectionId(string playerConnectionId)
    {
        var game = _games.FirstOrDefault(o => o.GetPlayerConnectionIds().Contains(playerConnectionId));
        if (game == null)
        {
            throw new Exception("Game not found");
        }

        return game;
    }
}