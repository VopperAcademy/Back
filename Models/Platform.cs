using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace vopperAcademyBackEnd.Models;

public class Platform
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string UrlImage { get; set; }
}