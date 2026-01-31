using CRMD.Domain.Tables;

namespace CRMD.Application.Common.Interfaces
{
    public interface ITableRepository
    {
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(int tableId);
        Task<List<TableDto>?> GetAllTables();
        Task<TableDto?> GetTableByIdAsync(int tableId);
        Task UpdateTableAsync(Table newTableData);
    }
}