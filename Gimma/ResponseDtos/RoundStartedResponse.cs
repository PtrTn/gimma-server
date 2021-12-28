using Gimma.Enums;

namespace Gimma.ResponseDtos;

public class RoundStartedResponse: IResponse
{
    private readonly string _prompt;
    private readonly List<string> _playerConnectionIds;

    public RoundStartedResponse(string prompt, List<string> playerConnectionIds)
    {
        _prompt = prompt;
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
        return new { prompt = _prompt };
    }
}