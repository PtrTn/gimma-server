namespace Gimma.Commands;

public class JoinGameCommand
{
    public readonly string UserName;
    public readonly string GameId;
    public readonly string HostConnectionId;

    public JoinGameCommand(string userName, string gameId, string hostConnectionId)
    {
        UserName = userName;
        GameId = gameId;
        HostConnectionId = hostConnectionId;
    }
}