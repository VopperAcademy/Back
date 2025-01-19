namespace vopperAcademyBackEnd.Models;

public class PlatformsDictionary
{
    public static string GetPlatform(string Id)
    {
        var PlatformIds = new Dictionary<string, string>
        {
            { "674ff374fb59f6e1f19a3baa", "Platzi" },
            {"67832932f258f6cb2c4a1e54", "Udemy"},
            {"678329cff258f6cb2c4a1e55", "EdTeam"},
            {"67832bbef258f6cb2c4a1e56", "Código Facilito"},
            {"67833173f258f6cb2c4a1e57", "DevTalles"}
        };
        
        return PlatformIds.ContainsKey(Id) ? PlatformIds[Id] : "Unknown";
    }
}