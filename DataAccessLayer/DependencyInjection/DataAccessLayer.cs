using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection;

public static class DataAccessLayer
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        return services;
    }
}