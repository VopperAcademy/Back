namespace vopperAcademyBackEnd.Models;

public class Chapter
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public required string Duration { get; set; }
    public required string Url { get; set; }
    public int Views { get; set; }
    public required string ImageUrl { get; set; }
}
