namespace Gimma.Commands;

public class CreateGameCommand
{
    public readonly string UserName;
    public readonly string HostConnectionId;

    public CreateGameCommand(string userName, string hostConnectionId)
    {
        UserName = userName;
        HostConnectionId = hostConnectionId;
    }
}