using CRMD.Domain.InventoryItems;

namespace CRMD.Application.Common.Interfaces
{
    public interface IInventoryItemRepository
    {
        Task AddItemToInventoryAsync(InventoryItem inventoryItem);
        Task DeleteItemFromInventoryAsync(int Id);
        Task<InventoryItem?> GetInventoryItemByIdAsync(int Id);
        Task<List<InventoryItem>?> GetAllInventoryItemsAsync();
        Task UpdateInventoryItem(InventoryItem newInventoryItemData);
    }
}