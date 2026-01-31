namespace CRMD.Application.Common.Interfaces;

public interface IOrderRepository
{
    public Task AddOrderAsync(Order order);
    public Task<List<Order>?> GetOrdersByDateAsync(DateTime date);
}