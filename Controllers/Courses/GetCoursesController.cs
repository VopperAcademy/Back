using System.Net;
using Microsoft.AspNetCore.Mvc;
using vopperAcademyBackEnd.Repository.Courses;

namespace vopperAcademyBackEnd.Controllers.Courses;

[ApiController]
[Route("api/courses")]
public class GetCoursesController : ControllerBase
{
    private readonly ICoursesRepository _coursesRepository;

    public GetCoursesController(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var response = await _coursesRepository.GetAllCoursesAsync();

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedCourses(string? platform, int pageNumber = 1)
    {
        var response = await _coursesRepository.GetPaginationCoursesAsync(pageNumber, platform);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilterCourses(string? stringSearch, string? platform, string? category,
        string? teacher)
    {
        var response = await _coursesRepository.GetFilterCoursesAsync(stringSearch, platform, category, teacher);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCoursesByIdAsync(string id)
    {
        var response = await _coursesRepository.GetCourseByIdAsync(id);
        
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("platform/{id}")]
    public async Task<IActionResult> GetCoursesByPlatform(string id)
    {
        var response = await _coursesRepository.GetCoursesByPlatformAsync(id);
        
        return StatusCode(response.StatusCode, response);
    }
}