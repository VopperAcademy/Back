using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace vopperAcademyBackEnd.Models;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<string> Categories { get; set; } = [];
    public List<Chapter> Chapters { get; set; } = [];
    public int ChaptersCount { get; set; }
    public required string ImageUrl { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Platform { get; set; }
    public required string Teacher { get; set; }
    public required string Date { get; set; }
}