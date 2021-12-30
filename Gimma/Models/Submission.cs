using Gimma.Commands;

namespace Gimma.Models;

public class Submission
{
    public readonly string connectionId;
    public readonly string _imageId;

    public Submission(string connectionId, string imageId)
    {
        this.connectionId = connectionId;
        _imageId = imageId;
    }
}