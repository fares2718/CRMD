using CRMD.Application.Common.Interfaces;
using CRMD.Domain.Orders;

namespace CRMD.Infrastructure.Orders.Persistence;

public class OrderRepository : IOrderRepository
{
    public Task AddOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}