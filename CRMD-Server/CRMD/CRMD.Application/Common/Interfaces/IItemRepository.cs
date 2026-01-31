using CRMD.Domain.Items;

namespace CRMD.Application.Common.Interfaces
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);
        Task DeleteItemAsync(int Id);
        Task<ItemDto?> GetItemByIdAsync(int Id);
        Task<List<ItemDto>?> GetAllItemsAsync();
        Task UpdateItemAsync(Item newItemData);
    }
}