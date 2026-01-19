using CRMD.Infrastructure.Employees;
using CRMD.Infrastructure.PerchaseInvoices.Persistence;

namespace CRMD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructures(
        this IServiceCollection services,
        string connectionString)
    {
        var dataSource = new NpgsqlDataSourceBuilder(connectionString)
            .MapComposite<RecipeItem>("restocafe.tprecipeitems")
            .MapComposite<OrderItems>("restocafe.tporderitems")
            .Build();
        services.AddSingleton(dataSource);
        services.AddScoped<IOrderRepository>(
            _ => new OrderRepository(connectionString));
        services.AddScoped<IMenuRepository>(
            _ => new MenuRepository(connectionString));
        services.AddScoped<IEmployeeRepository>(
            _ => new EmployeeRepository(connectionString));
        services.AddScoped<IPerchaseInvoiceRepository>(
            _ => new PerchaseInvoiceRepository(connectionString)
        );
        return services;
    }
}