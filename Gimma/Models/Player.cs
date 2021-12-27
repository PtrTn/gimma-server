namespace Gimma.Models;

public class Player
{
    public readonly string _userName;
    public readonly string _connectionId;
    public readonly bool _isHost;

    public Player(string userName, string connectionId, bool isHost)
    {
        _userName = userName;
        _connectionId = connectionId;
        _isHost = isHost;
    }
}