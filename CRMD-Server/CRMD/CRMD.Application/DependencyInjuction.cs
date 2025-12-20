using CRMD.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CRMD.Application;

public static class DependencyInjuction
{
    public static IServiceCollection AddApplications(this IServiceCollection services)
    { 
        services.AddScoped<IOrdersService, OrderService>();
        return services;
    }
}