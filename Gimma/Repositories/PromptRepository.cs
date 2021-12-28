namespace Gimma.Repositories;

public class PromptRepository
{
    private static Random random = new Random();
    private readonly List<string> _prompts  = new List<string>
    {
        "A bald cyclist races past you at full speed and smashes into a boy in a wheelchair",
        "You meet your neighbour, who has been living on nothing but take-away and monster energy, for the first time in weeks",
        "When walking around in the gym you accidentally brush shoulders with a very muscular dwarf",
        "You're sitting on a park bench minding your own business, when suddenly a guy sits next to you and stabs a needle in his thigh",
        "Driving around on your tuned moped you turn a corner and run into an albino-looking cop who points to a roller bench",
    };
    
    public List<string> GetPrompts(int amount)
    {
        if (amount > _prompts.Count)
        {
            throw new Exception("Not enough unique prompts");
        }
        return _prompts.OrderBy(x => random.Next()).Take(amount).ToList();
    }
}