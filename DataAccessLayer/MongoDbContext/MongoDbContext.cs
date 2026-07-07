using BusinessLogicLayer.Entities;
using MongoDB.Driver;

namespace DataAccessLayer.MongoDbContext;

public class MongoDbContext
{
   private readonly IMongoDatabase _database;
    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        Console.WriteLine($"MongoDB connection successful to {databaseName}");
    }
    
    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
}