using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer.DependencyInjection;

public static class DependencyInjection
{

    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        return services;
    }
}