namespace vopperAcademyBackEnd.Models.DTOs.Request.Courses;

public class CourseRequestDTO
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<string> Categories { get; set; } = [];
    public required string ImageUrl { get; set; }
    public required string PlatformId { get; set; }
    public required string Teacher { get; set; }
    public required string Date { get; set; }
    
    
}