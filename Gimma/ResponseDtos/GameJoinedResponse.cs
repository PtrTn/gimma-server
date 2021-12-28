using Gimma.Enums;

namespace Gimma.ResponseDtos;

public class GameJoinedResponse: IResponse
{
    private readonly string _playerConnectionId;

    public GameJoinedResponse(string playerConnectionId)
    {
        _playerConnectionId = playerConnectionId;
    }
    
    public ResponseEventMethod GetMethod()
    {
        return ResponseEventMethod.GameJoined;
    }

    public List<string> GetConnectionIds()
    {
        return new List<string> {_playerConnectionId};
    }

    public object GetData()
    {
        return new { };
    }
}