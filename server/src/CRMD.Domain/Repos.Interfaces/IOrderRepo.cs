using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IOrderRepo
{
    public Task<int> AddOrderItemAsync(Queue<clsOrderItem> orderItems);
    public Task<int> CreateOrderAsync(clsOrder order);
    public Task<bool> DeleteOrderItemAsync(int orderItemId, int orderId);
    public Task<bool> FinalizeOrderAsync(int orderId);
    public Task<clsOrder?> GetOrderByIdAsync(int orderId);
    public Task<List<clsOrderItem>> GetOrderItemsByAsync(int orderId);
}
