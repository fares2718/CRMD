namespace CRMD.Infrastructure.Menu
{
    public class MenuRepository : IMenuRepository
    {
        private readonly string _connectionString;

        public MenuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (var cmd = new NpgsqlCommand("addmenuitem", conn))
                {
                    //ToDo: Implement AddMenuItemAsync method
                }

            }
        }

        public async Task<List<MenuItemDto>> GetAllMenuItemsAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("getmenuitems", conn))
                {
                    var menuItems = new List<MenuItemDto>();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var menuItem = Mapper.Map<MenuItemDto>(reader);
                            menuItems.Add(menuItem);
                        }
                    }
                    return menuItems;
                }
            }
        }
    }
}