using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IMenuItemRepo
{
    public Task<int> AddMenuItemAsync(clsMenuItem menuItem);
    public Task<bool> AvailableMenuItemAsync(int menuItemId);
    public Task<List<clsMenuItem>> GetAllMenuItemsAsync();
    public Task<clsMenuItem?> GetMenuItemByIdAsync(int menuItemId);
    public Task<bool> UpdateMenuItemPriceAsync(int menuItemId, decimal newPrice);
    public Task<bool> UnavailableMenuItemAsync(int menuItemId);
}
