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

    [HttpGet("{idCourse}/{chapterPosition}")]
    public async Task<IActionResult> GetChapterAsync(string idCourse, int chapterPosition)
    {
        var response = await _chaptersRepository.GetChapterAsync(idCourse, chapterPosition);
        
        return StatusCode(response.StatusCode, response);
    }

}