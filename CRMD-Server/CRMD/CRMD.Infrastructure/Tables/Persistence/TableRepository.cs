using CRMD.Domain.Tables;
using CRMD.Infrastructure.Generics;

namespace CRMD.Infrastructure.Tables.Persistence
{
    public class TableRepository : ITableRepository
    {
        private readonly string _connectionString;

        public TableRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddTableAsync(Table table)
        {
            await GenericRepository<Table>
            .AddAsync(table, _connectionString, "restocafe.addtable");
        }

        public async Task DeleteTableAsync(int tableId)
        {
            await GenericRepository<Table>
            .DeleteAsync(tableId, _connectionString, "restocafe.deletetable");
        }

        public async Task<List<TableDto>?> GetAllTables()
        {
            var tables = new List<TableDto>();
            using (var reader = await GenericRepository<TableDto>
            .GetAllAsync(_connectionString, "restocafe.gettables()"))
            {
                if (reader == null || !reader.HasRows)
                    return null;
                while (await reader.ReadAsync())
                {
                    var table = Mapper.Map<TableDto>(reader);
                    tables.Add(table);
                }
            }
            NpgsqlConnection.ClearAllPools();
            return tables.OrderBy(t => t.TableId).ToList();
        }

        public async Task<TableDto?> GetTableByIdAsync(int tableId)
        {
            using (var reader = await GenericRepository<Table>
            .GetByIdAsync(tableId, _connectionString, "restocafe.gettablebyid(@id)"))
            {
                TableDto table = new TableDto();
                if (reader == null || !reader.HasRows)
                    return null;
                table = Mapper.Map<TableDto>(reader);
                NpgsqlConnection.ClearAllPools();
                return table;
            }
        }

        public async Task UpdateTableAsync(Table newTableData)
        {
            await GenericRepository<Table>
            .UpdateAsync(newTableData, _connectionString, "restocafe.updatetable");
        }
    }
}