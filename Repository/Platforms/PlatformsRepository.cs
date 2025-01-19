using System.Collections.ObjectModel;
using MongoDB.Bson;
using MongoDB.Driver;
using vopperAcademyBackEnd.Data;
using vopperAcademyBackEnd.Models;
using vopperAcademyBackEnd.Models.DTOs;
using vopperAcademyBackEnd.Models.DTOs.Response.Platforms;

namespace vopperAcademyBackEnd.Repository.Platforms;

public class PlatformsRepository : IPlatformsRepository
{
    private readonly MongoDbContext _context;

    public PlatformsRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<DynamicResponse<List<PreviewPlatformResponseDTO>>> GetPlatformsAsync()
    {
        try
        {
            var projection = Builders<Platform>.Projection.Expression(p => new PreviewPlatformResponseDTO()
            {
                Id = p.Id!,
                Name = p.Name,
                UrlImage = p.UrlImage,
                Description = p.Description
            });
            
            var platforms = await _context.Platforms.Find(new BsonDocument()).Project(projection).ToListAsync();
            
            return platforms == null || platforms.Count == 0 ? DynamicResponse<List<PreviewPlatformResponseDTO>>.CreateError("No se encontraron plataformas.", 404)
                : DynamicResponse<List<PreviewPlatformResponseDTO>>.CreateSuccess(platforms);
        }
        catch (Exception e)
        {
            return DynamicResponse<List<PreviewPlatformResponseDTO>>.CreateError($"Ocurrió un error al paginar los cursos. \n Error: {e.Message}");
        }
    }
    
}