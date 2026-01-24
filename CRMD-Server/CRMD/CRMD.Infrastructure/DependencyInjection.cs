using CRMD.Infrastructure.Employees;
using CRMD.Infrastructure.InventoryItems.Persistence;
using CRMD.Infrastructure.Items.Persistence;
using CRMD.Infrastructure.PerchaseInvoices.Persistence;
using CRMD.Infrastructure.Suppliers.Persistence;

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
        services.AddScoped<ISupplierRepository>(
            _ => new SupplierRepository(connectionString)
        );
        services.AddScoped<IItemRepository>(
            _ => new ItemRepository(connectionString)
        );
        services.AddScoped<IInventoryItemRepository>(
            _ => new InventoryItemRepository(connectionString)
        );
        return services;
    }
}