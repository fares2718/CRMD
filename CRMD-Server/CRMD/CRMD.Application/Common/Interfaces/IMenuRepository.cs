namespace CRMD.Application.Common.Interfaces
{
    public interface IMenuRepository
    {
        public Task AddMenuItemAsync(MenuItem menuItem);
        public Task<List<MenuItemDto>?> GetAllMenuItemsAsync();
        Task<MenuItemDto?> GetMenuItemByIdAsync(int Id);
        public Task UpdateRecipeAsync(Recipe recipe);
    }
}