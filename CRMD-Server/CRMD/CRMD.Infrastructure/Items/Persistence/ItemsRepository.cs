using CRMD.Domain.Items;
using CRMD.Infrastructure.Generics;

namespace CRMD.Infrastructure.Items.Persistence
{
    internal class ItemsRepository
    {
        private readonly string _connectionString;

        public ItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddItemAsync(Item item)
        {
            await GenericRepository<Item>.AddAsync(item, _connectionString, "inventory.additem");
        }

        public async Task DeleteItemAsync(int Id)
        {
            await GenericRepository<Item>.DeleteAsync(Id, _connectionString, "inventory.deleteitem");
        }

        public async Task<ItemDto?> GetItemByIdAsync(int Id)
        {
            using (var reader = await GenericRepository<ItemDto>
            .GetByIdAsync(Id, _connectionString, "getitembyid(@id)"))
            {
                ItemDto item = new ItemDto();
                if (reader != null && await reader.ReadAsync())
                {
                    item = Mapper.Map<ItemDto>(reader);
                }
                NpgsqlConnection.ClearAllPools();
                return item;
            }
        }

        public async Task<List<ItemDto>> GetAllItemsAsync()
        {
            var items = new List<ItemDto>();
            using (var reader = await GenericRepository<ItemDto>
            .GetAllAsync(_connectionString, "getitems()"))
            {
                while (await reader.ReadAsync())
                {
                    var item = Mapper.Map<ItemDto>(reader);
                    items.Add(item);
                }
                NpgsqlConnection.ClearAllPools();
                return items;
            }
        }
    }
}