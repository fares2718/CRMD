using CRMD.Application.Common.Interfaces;
using CRMD.Domain.Orders;

namespace CRMD.Infrastructure.Orders.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task AddOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}