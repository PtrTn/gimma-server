namespace Gimma.Repositories;

public class RandomStringRepository
{
    private static Random random = new Random();
    
    public string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(
            Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)])
            .ToArray()
        );
    }
}