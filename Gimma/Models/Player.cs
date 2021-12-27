namespace Gimma.Models;

public class Player
{
    public readonly string _userName;
    public readonly string _connectionId;

    public Player(string userName, string connectionId)
    {
        _userName = userName;
        _connectionId = connectionId;
    }
}