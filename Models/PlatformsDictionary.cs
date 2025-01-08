namespace vopperAcademyBackEnd.Models;

public class PlatformsDictionary
{
    public static string GetPlatform(string Id)
    {
        var PlatformIds = new Dictionary<string, string>
        {
            { "674ff374fb59f6e1f19a3baa", "Platzi" }
        };
        
        return PlatformIds.ContainsKey(Id) ? PlatformIds[Id] : "Unknown";
    }
}