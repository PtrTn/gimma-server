namespace Gimma.Repositories;

public class PromptRepository
{
    private static Random random = new Random();
    private readonly List<string> _prompts  = new List<string>
    {
        "Je ziet een fietser met volle snelheid tegen een jongen in een rolstoel klappen",
        "Je komt je buurman, die al weken alleen maar op thuisbezorgd en monster, voor het eerst in weken tegen",
        "Je loopt in de sportschool per ongeluk tegen de schouder van die kleine veel te gespierde man aan",
        "Je zit rustig op een bankje in het park, komt er opeens een kerel met een opa pet naast je zitten die een naald in z'n been zet",
        "Je rijdt lekker op je opgevoerde scooter tot een Albino politie agent je naar een rollerbank stuurt"
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