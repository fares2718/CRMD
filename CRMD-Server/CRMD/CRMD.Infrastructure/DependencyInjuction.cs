using CRMD.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CRMD.Infrastructure;

public static class DependencyInjuction
{
    public static IServiceCollection AddInfrastructures(this IServiceCollection services)
    {
        return services;
    }
}