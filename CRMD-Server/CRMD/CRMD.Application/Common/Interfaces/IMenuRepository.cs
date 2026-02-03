using CRMD.Domain.Categories;

namespace CRMD.Application.Common.Interfaces
{
    public interface IMenuRepository
    {
        Task AddMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(int Id);
        Task<List<MenuItemDto>?> GetAllMenuItemsAsync();
        Task<List<Category>?> GetMenuCategoriesAsync();
        Task<MenuItemDto?> GetMenuItemByIdAsync(int Id);
        Task UpdateRecipeAsync(Recipe recipe);
    }
}