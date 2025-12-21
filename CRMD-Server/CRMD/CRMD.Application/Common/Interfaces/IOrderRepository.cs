using CRMD.Domain.Orders;

namespace CRMD.Application.Common.Interfaces;

public interface IOrderRepository
{
    public Task AddOrderAsync(Order order);
}