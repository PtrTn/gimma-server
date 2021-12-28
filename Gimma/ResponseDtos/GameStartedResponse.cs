using Gimma.Enums;

namespace Gimma.ResponseDtos;

public class GameStartedResponse: IResponse
{
    private readonly List<string> _playerConnectionIds;

    public GameStartedResponse(List<string> playerConnectionIds)
    {
        _playerConnectionIds = playerConnectionIds;
    } 
    
    public ResponseEventMethod GetMethod()
    {
        return ResponseEventMethod.GameStarted;
    }

    public List<string> GetConnectionIds()
    {
        return _playerConnectionIds;
    }

    public object GetData()
    {
        return new { };
    }
}