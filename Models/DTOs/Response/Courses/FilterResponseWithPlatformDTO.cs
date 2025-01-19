namespace vopperAcademyBackEnd.Models.DTOs.Response.Courses;

public class FilterResponseWithPlatformDTO<T>
{
    public List<T> FilterCourses { get; set; } = [];
    public int TotalCourses { get; set; }
    public required string NamePlatform { get; set; }
    public required string DescriptionPlatform { get; set; }
    public string? ImagePlatform { get; set; }
}