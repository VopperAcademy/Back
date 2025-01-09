using Microsoft.AspNetCore.Mvc;
using vopperAcademyBackEnd.Repository.Chapters;

namespace vopperAcademyBackEnd.Controllers.Chapters;

[ApiController]
[Route("api/chapters")]
public class GetChaptersController : ControllerBase
{
    private readonly IChaptersRepository _chaptersRepository;

    public GetChaptersController(IChaptersRepository chaptersRepository)
    {
        _chaptersRepository = chaptersRepository;
    }

    [HttpGet("{idCourse}/{chapter}")]
    public async Task<IActionResult> GetChapterAsync(string idCourse, int chapter)
    {
        var response = await _chaptersRepository.GetChapterAsync(idCourse, chapter);
        
        return StatusCode(response.StatusCode, response);
    }

}