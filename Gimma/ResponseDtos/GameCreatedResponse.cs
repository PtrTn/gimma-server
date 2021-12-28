using Gimma.Enums;

namespace Gimma.ResponseDtos;

public class GameCreatedResponse: IResponse
{
    private readonly string _gameId;
    private readonly string _hostConnectionId;

    public GameCreatedResponse(string gameId, string hostConnectionId)
    {
        _gameId = gameId;
        _hostConnectionId = hostConnectionId;
    }

    public ResponseEventMethod GetMethod()
    {
        return ResponseEventMethod.GameCreated;
    }

    public List<string> GetConnectionIds()
    {
        return new List<string> {_hostConnectionId};
    }
    
    public object GetData()
    {
        return new { gameId = _gameId };
    }
}