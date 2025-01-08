using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Models.DTOs;
using vopperAcademyBackEnd.Models.DTOs.Response.Platforms;

namespace vopperAcademyBackEnd.Repository.Platforms;

public interface IPlatformsRepository
{
    Task<DynamicResponse<List<PreviewPlatformResponseDTO>>> GetPlatformsAsync();
    Task<DynamicResponse<Platform>> GetPlatformByIdAsync(string id);
}