using CRMD.Application.Common.Interfaces;
using CRMD.Infrastructure.Orders.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CRMD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructures(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}