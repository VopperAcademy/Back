using Microsoft.AspNetCore.Mvc;
using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Models.DTOs.Request.Courses;
using vopperAcademyBackEnd.Repository.Courses;

namespace vopperAcademyBackEnd.Controllers.Courses;

[ApiController]
[Route("api/courses")]
public class PostCoursesController : ControllerBase
{
    private readonly ICoursesRepository _coursesRepository;

    public PostCoursesController(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CourseRequestDTO courseRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);  // Devuelve los errores de validación
        }
        
        var response = await _coursesRepository.PostCourseAsync(courseRequestDto);
        
        return StatusCode(response.StatusCode, response);
    }
}