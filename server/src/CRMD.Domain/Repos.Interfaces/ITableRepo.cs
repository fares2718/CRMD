using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface ITableRepo
{
    public Task<int> AddTableAsync(clsTable table);
    public Task<bool> AssignWaiterToTableAsync(int tableId, string waiterId);
    public Task<List<clsTable>> GetAllTablesAsync();
    public Task<bool> MergeTablesAsync(int sourceTableId, int targetTableId);
    public Task<bool> UpdateTableStatusAsync(int tableId, short status);
}
