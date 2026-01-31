using CRMD.Infrastructure.Generics;


namespace CRMD.Infrastructure.Menu
{
    internal class MenuRepository : IMenuRepository
    {
        private readonly string _connectionString;

        public MenuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("restocafe.addmenuitem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, menuItem.Name);
                    cmd.Parameters.Add("price", NpgsqlDbType.Money).Value = menuItem.Price;
                    cmd.Parameters.AddWithValue("categoryid", menuItem.CategoryId);
                    var itemsJson = JsonSerializer.Serialize(menuItem.Recipe.Ingredients);
                    cmd.Parameters.AddWithValue("ingredients", NpgsqlDbType.Jsonb, itemsJson);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }

            }*/
            await GenericRepository<MenuItem>.AddAsync(menuItem, _connectionString, "restocafe.addmenuitem");
        }

        public async Task DeleteMenuItemAsync(int Id)
        {
            await GenericRepository<MenuItem>.DeleteAsync(Id, _connectionString, "restocafe.deletemenuitem");
        }

        public async Task<List<MenuItemDto>?> GetAllMenuItemsAsync()
        {
            var menuItems = new List<MenuItemDto>();
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("select * from restocafe.getallmenuitems()", conn))
                {
                    
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
            }*/
            using (var reader = await GenericRepository<List<EmployeeDto>>.GetAllAsync(_connectionString, "restocafe.getallmenuitems()"))
            {
                if (reader == null || !reader.HasRows)
                    return null;
                while (await reader.ReadAsync())
                {
                    var menuItem = Mapper.Map<MenuItemDto>(reader);
                    menuItems.Add(menuItem);
                }
                NpgsqlConnection.ClearAllPools();
                return menuItems;
            }
        }

        public async Task<MenuItemDto?> GetMenuItemByIdAsync(int Id)
        {
            using (var reader = await GenericRepository<MenuItemDto>
            .GetByIdAsync(Id, _connectionString, "restocafe.getmenuitembyid(@id)"))
            {
                MenuItemDto item = new MenuItemDto();
                if (reader == null || !reader.HasRows)
                    return null;
                item = Mapper.Map<MenuItemDto>(reader);
                NpgsqlConnection.ClearAllPools();
                return item;
            }
        }

        public async Task UpdateRecipeAsync(Recipe newRecipe)
        {
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("updaterecipe", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("recipeitems",
                    recipeItems.ToArray());
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }*/
            await GenericRepository<Recipe>.UpdateAsync(newRecipe, _connectionString, "restocafe.updaterecipe");
        }
    }
}