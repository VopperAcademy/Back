using Microsoft.AspNetCore.Mvc;
using vopperAcademyBackEnd.Repository.Platforms;

namespace vopperAcademyBackEnd.Controllers.Platforms;

[ApiController]
[Route("api/platforms")]
public class GetPlatformsController : ControllerBase
{
    private readonly IPlatformsRepository _platformsRepository;

    public GetPlatformsController(IPlatformsRepository platformsRepository)
    {
        _platformsRepository = platformsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlatformsAsync()
    {
        var response = await _platformsRepository.GetPlatformsAsync();

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlatformAsync(string id)
    {
        var response = await _platformsRepository.GetPlatformByIdAsync(id);
        
        return StatusCode(response.StatusCode, response);
    }
}