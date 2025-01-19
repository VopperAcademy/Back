using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Models.DTOs;
using vopperAcademyBackEnd.Models.DTOs.Response;
using vopperAcademyBackEnd.Models.DTOs.Response.Courses;
using vopperAcademyBackEnd.Models.DTOs.Response.Platforms;

namespace vopperAcademyBackEnd.Repository.Courses;

public interface ICoursesRepository
{
    //GET
    Task<DynamicResponse<List<PreviewCourseResponseDTO>>> GetAllCoursesAsync();
    Task<DynamicResponse<PaginatedResponseDto<PreviewCourseResponseDTO>>> GetPaginationCoursesAsync(int pageNumber,string? platform);
    Task<DynamicResponse<FilterResponseDTO<PreviewCourseResponseDTO>>> GetFilterCoursesAsync(string? stringSearch, string? platform, string? category, string? teacher);
    Task<DynamicResponse<Course>> GetCourseByIdAsync(string id);
    Task<DynamicResponse<FilterResponseWithPlatformDTO<PreviewCourseResponseDTO>>> GetCoursesByPlatformAsync(string idPlatform);
    
}