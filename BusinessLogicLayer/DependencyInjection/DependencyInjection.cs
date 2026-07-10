
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogicLayer.Validators;

namespace BusinessLogicLayer.DependencyInjection;

public static class DependencyInjection
{

    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {

        services.AddScoped<IValidator, OrderValidator>();
        return services;
    }
}