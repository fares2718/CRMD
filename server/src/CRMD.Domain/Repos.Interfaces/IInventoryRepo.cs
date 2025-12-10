using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IInventoryRepo
{
    public Task AddInventoryItemAsync(clsInventory objInventory);
    public Task<int> AddInventoryTransactionAsync(clsInventoryTransaction objInventoryTransaction);
    public Task<List<clsInventory>> GetAllInventrotyItemsAsync();
    public Task<clsInventory?> GetInventoryItemByIdAsync(int itemId);
    public Task<List<clsInventoryTransaction>> GetInventoryTransactionByItemAsync(int ItemId);
    public Task<List<clsInventoryTransaction>> GetInventoryTransactionsReportAsync(DateTime startDate, DateTime endDate);
}
