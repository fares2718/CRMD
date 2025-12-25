using CRMD.Application.Common.Interfaces;
using CRMD.Infrastructure.Menu;
using CRMD.Infrastructure.Orders.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CRMD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructures(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddScoped<IOrderRepository>(
            _ => new OrderRepository(connectionString));
        services.AddScoped<IMenuRepository>(
            _ => new MenuRepository(connectionString));
        return services;
    }
}