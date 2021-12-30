using Gimma.Enums;
using Gimma.Models;

namespace Gimma.ResponseDtos;

public class RoundCompletedResponse: IResponse
{
    private readonly List<Submission> _submssions;
    private readonly List<string> _playerConnectionIds;

    public RoundCompletedResponse(List<Submission> submssions, List<string> playerConnectionIds)
    {
        _submssions = submssions;
        _playerConnectionIds = playerConnectionIds;
    } 
    
    public ResponseEventMethod GetMethod()
    {
        return ResponseEventMethod.RoundCompleted;
    }

    public List<string> GetConnectionIds()
    {
        return _playerConnectionIds;
    }

    public object GetData()
    {
        return new
        {
            Submissions =_submssions.Select(s => new
            {
                Player = s.connectionId, // todo, map to username
                Submission = s._imageId
            }),
        };
    }
}