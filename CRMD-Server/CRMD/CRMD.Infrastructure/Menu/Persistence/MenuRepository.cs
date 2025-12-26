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
                using (var cmd = new NpgsqlCommand("addmenuitem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", menuItem.Name);
                    cmd.Parameters.AddWithValue("price", menuItem.Price);
                    cmd.Parameters.AddWithValue("categoryid", menuItem.CategoryId);
                    cmd.Parameters.AddWithValue("ingredients", menuItem.Recipe.
                    Ingredients.ToArray());
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
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

        public async Task UpdateRecipeAsync(List<RecipeItem> recipeItems)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("updaterecipe", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("recipeitems",
                    recipeItems.ToArray());
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}