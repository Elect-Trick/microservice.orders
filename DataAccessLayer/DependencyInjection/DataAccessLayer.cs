using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection;

public static class DataAccessLayer
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddSingleton(new MongoDbContext.MongoDbContext(connectionString: "mongodb://localhost:27017",
            databaseName: "OrdersDB"));
        return services;
    }
}