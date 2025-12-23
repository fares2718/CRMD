using CRMD.Application.Orders.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CRMD.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplications(this IServiceCollection services)
    { 
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });
        return services;
    }
}