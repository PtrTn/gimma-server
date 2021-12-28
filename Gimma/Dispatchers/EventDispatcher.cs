using Gimma.Hubs;
using Gimma.ResponseDtos;
using Microsoft.AspNetCore.SignalR;

namespace Gimma.Dispatchers;

public class EventDispatcher
{
    private readonly IHubContext<GameHub> _hubContext;

    public EventDispatcher(IHubContext<GameHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task Dispatch(IResponse response)
    {
        await _hubContext.Clients
            .Clients(response.GetConnectionIds())
            .SendAsync(
                response.GetMethod().ToString(),
                response.GetData()
            );
    }
}