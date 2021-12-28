namespace Gimma.Commands;

public class StartGameCommand
{
    public readonly string HostConnectionId;

    public StartGameCommand(string hostConnectionId)
    {
        HostConnectionId = hostConnectionId;
    }
}