using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace vopperAcademyBackEnd.Models.DTOs.Response.Courses;

public class PreviewCourseResponseDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    public required string Title { get; set; }
    public List<string>? Categories { get; set; }
    public required string Teacher { get; set; }
    public required string ImageUrl { get; set; }
    
}