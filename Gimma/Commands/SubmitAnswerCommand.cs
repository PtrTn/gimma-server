namespace Gimma.Commands;

public class SubmitAnswerCommand
{
    public readonly string ImageId;
    public readonly string PlayerConnectionId;

    public SubmitAnswerCommand(string imageId, string playerConnectionId)
    {
        ImageId = imageId;
        PlayerConnectionId = playerConnectionId;
    }
}