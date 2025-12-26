namespace CRMD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructures(
        this IServiceCollection services,
        string connectionString)
    {
        var dataSource = new NpgsqlDataSourceBuilder(connectionString)
            .MapComposite<RecipeItem>("tprecipeitems")
            .MapComposite<OrderItems>("tporderitems")
            .Build();
        services.AddSingleton(dataSource);
        services.AddScoped<IOrderRepository>(
            _ => new OrderRepository(connectionString));
        services.AddScoped<IMenuRepository>(
            _ => new MenuRepository(connectionString));
        return services;
    }
}