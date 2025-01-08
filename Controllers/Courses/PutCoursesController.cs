using Microsoft.AspNetCore.Mvc;
using vopperAcademyBackEnd.Repository.Courses;

namespace vopperAcademyBackEnd.Controllers.Courses;

[ApiController]
[Route("api/courses")]
public class PutCoursesController : ControllerBase
{
    private readonly ICoursesRepository _coursesRepository;

    public PutCoursesController(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    
    
}