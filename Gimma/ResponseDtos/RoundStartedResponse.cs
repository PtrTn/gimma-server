using Gimma.Enums;

namespace Gimma.ResponseDtos;

public class RoundStartedResponse: IResponse
{
    private readonly string _prompt;
    private readonly List<string> _imageIds;
    private readonly string _playerConnectionId;

    public RoundStartedResponse(string prompt, List<string> imageIds, string playerConnectionId)
    {
        _prompt = prompt;
        _imageIds = imageIds;
        _playerConnectionId = playerConnectionId;
    } 
    
    public ResponseEventMethod GetMethod()
    {
        return ResponseEventMethod.GameStarted;
    }

    public List<string> GetConnectionIds()
    {
        return new List<string> { _playerConnectionId };
    }

    public object GetData()
    {
        return new
        {
            prompt = _prompt,
            imageIds = _imageIds,
        };
    }
}