namespace vopperAcademyBackEnd.Models.DTOs.Response.Courses;

public class PaginatedResponseDto<T>
{
    public List<T> ListCourses { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}