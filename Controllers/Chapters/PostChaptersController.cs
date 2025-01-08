using Microsoft.AspNetCore.Mvc;
using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Repository.Chapters;

namespace vopperAcademyBackEnd.Controllers.Chapters;

[ApiController]
[Route("api/chapters")]
public class PostChaptersController : ControllerBase
{
    private readonly IChaptersRepository _chaptersRepository;

    public PostChaptersController(IChaptersRepository chaptersRepository)
    {
        _chaptersRepository = chaptersRepository;
    }

    [HttpPost("{idCourse}")]
    public async Task<IActionResult> PostChapterAsync([FromBody] Chapter chapter ,string idCourse)
    {
        var responde = await _chaptersRepository.PostChapterAsync(chapter, idCourse);

        return StatusCode(responde.StatusCode, responde);
    }
}