using BusinessLogicLayer.Entities;
using MongoDB.Driver;

namespace DataAccessLayer.MongoDbContext;

public class MongoDbContext
{
   private readonly IMongoDatabase _database;
    public MongoDbContext(string connectionString, string databaseName)
    {
        
        var client = new MongoClient(connectionString.Replace("$MONGO_DB_HOST",Environment.GetEnvironmentVariable("MONGO_DB_HOST") ?? "localhost").Replace("$MONGO_DB_PORT", Environment.GetEnvironmentVariable("MONGO_DB_PORT") ?? "27017"));
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
}