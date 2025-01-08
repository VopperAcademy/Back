using Microsoft.AspNetCore.Mvc;
using vopperAcademyBackEnd.Repository.Chapters;

namespace vopperAcademyBackEnd.Controllers.Chapters;

[ApiController]
[Route("api/chapters")]
public class PutChaptersController : ControllerBase
{
    private readonly IChaptersRepository _chaptersRepository;

    public PutChaptersController(IChaptersRepository chaptersRepository)
    {
        _chaptersRepository = chaptersRepository;
    }
    
    [HttpPut("{idCourse}/{chapter}/views")]
    public async Task<IActionResult> UpdateViewsInChapterAsync(string idCourse, int chapter)
    {
        var response = await _chaptersRepository.PutViewsInChapterAsync(idCourse, chapter);

        return StatusCode(response.StatusCode, response);
    }
}