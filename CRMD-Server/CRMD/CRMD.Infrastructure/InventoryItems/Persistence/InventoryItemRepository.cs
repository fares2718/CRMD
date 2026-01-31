using CRMD.Domain.InventoryItems;
using CRMD.Infrastructure.Generics;

namespace CRMD.Infrastructure.InventoryItems.Persistence
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly string _connectionString;

        public InventoryItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddItemToInventoryAsync(InventoryItem inventoryItem)
        {
            await GenericRepository<InventoryItem>.AddAsync(inventoryItem, _connectionString, "inventory.addinventoryitem");
        }

        public async Task DeleteItemFromInventoryAsync(int Id)
        {
            await GenericRepository<InventoryItem>.DeleteAsync(Id, _connectionString, "inventory.deleteinventoryitem");
        }

        public async Task<List<InventoryItem>?> GetAllInventoryItemsAsync()
        {
            var inventoryItems = new List<InventoryItem>();
            using (var reader = await GenericRepository<InventoryItem>
            .GetAllAsync(_connectionString, "inventory.getinventoryitems"))
            {
                if (reader == null || !reader.HasRows)
                    return null;
                while (await reader.ReadAsync())
                {
                    var inventoryItem = Mapper.Map<InventoryItem>(reader);
                    inventoryItems.Add(inventoryItem);
                }
                NpgsqlConnection.ClearAllPools();
                return inventoryItems;
            }
        }

        public async Task<InventoryItem?> GetInventoryItemByIdAsync(int Id)
        {
            using (var reader = await GenericRepository<InventoryItem>
            .GetAllAsync(_connectionString, "inventory.getinventoryitembyid(@id)"))
            {
                InventoryItem inventoryItem = new InventoryItem();
                if (reader == null || !reader.HasRows)
                    return null;
                inventoryItem = Mapper.Map<InventoryItem>(reader);
                NpgsqlConnection.ClearAllPools();
                return inventoryItem;
            }
        }

        public async Task UpdateInventoryItem(InventoryItem newInventoryItemData)
        {
            await GenericRepository<InventoryItem>.UpdateAsync(newInventoryItemData, _connectionString, "inventory.updateinventoryitem");
        }
    }
}