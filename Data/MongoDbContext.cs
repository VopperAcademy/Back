using System.Security.Authentication;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using vopperAcademyBackEnd.Models;

namespace vopperAcademyBackEnd.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<DatabaseSettings> databaseSettings)
    {
        var settings = MongoClientSettings.FromUrl(new MongoUrl(
            databaseSettings.Value.ConnectionString
        ));
        
        settings.SslSettings = new SslSettings
        {
            CheckCertificateRevocation = false,
            EnabledSslProtocols = SslProtocols.Tls12
        };
        
        settings.ConnectTimeout = TimeSpan.FromSeconds(30);
        settings.SocketTimeout = TimeSpan.FromSeconds(30);

        MongoClient mongoClient = new(settings);
        _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
    }

    public IMongoCollection<Course> Courses => _database.GetCollection<Course>("Courses");
    public IMongoCollection<Platform> Platforms => _database.GetCollection<Platform>("Platforms");
}