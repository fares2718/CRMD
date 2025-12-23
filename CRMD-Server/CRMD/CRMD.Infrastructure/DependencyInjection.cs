using CRMD.Application.Common.Interfaces;
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
        return services;
    }
}