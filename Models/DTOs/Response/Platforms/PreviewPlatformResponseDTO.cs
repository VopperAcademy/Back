using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace vopperAcademyBackEnd.Models.DTOs.Response.Platforms;

public class PreviewPlatformResponseDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string UrlImage { get; set; }
}