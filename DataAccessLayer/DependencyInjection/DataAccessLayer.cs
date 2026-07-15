using BusinessLogicLayer.RepositoryContracts;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DataAccessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection;

public static class DataAccessLayer
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        
        var host = Environment.GetEnvironmentVariable("MONGO_DB_HOST") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("MONGO_DB_PORT") ?? "27017";
        var connectionString = $"mongodb://{host}:{port}";
        var databaseName = Environment.GetEnvironmentVariable("MONGO_DB_NAME") ?? "OrdersDB";
        services.AddSingleton(new MongoDbContext.MongoDbContext(connectionString: connectionString,
            databaseName: "OrdersDB"));

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(MappingProfiles));
        });
        return services;
    }
}