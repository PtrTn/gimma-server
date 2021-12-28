using Gimma.Enums;

namespace Gimma.ResponseDtos;

public interface IResponse
{
    public ResponseEventMethod GetMethod();

    public List<string> GetConnectionIds();
    
    public object GetData();
}