namespace vopperAcademyBackEnd.Models.DTOs.Response.Courses;

public class FilterResponseDTO<T>
{
    public List<T> FilterCourses { get; set; } = [];
    public int TotalCourses { get; set; }
}