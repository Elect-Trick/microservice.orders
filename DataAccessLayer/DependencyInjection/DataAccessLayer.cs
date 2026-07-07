using BusinessLogicLayer.RepositoryContracts;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Repositories;
using DataAccessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection;

public static class DataAccessLayer
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddSingleton(new MongoDbContext.MongoDbContext(connectionString: "mongodb://localhost:27017",
            databaseName: "OrdersDB"));

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}