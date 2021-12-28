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
        var game = _games.FirstOrDefault(o => o._gameId == gameId);
        if (game == null)
        {
            throw new Exception("Game not found");
        }

        return game;
    }

    public Game FetchByHostConnectionId(string hostConnectionId)
    {
        var game = _games.FirstOrDefault(o => o._host._connectionId == hostConnectionId);
        if (game == null)
        {
            throw new Exception("Game not found");
        }

        return game;
    }
}