using CRMD.Application.Generics.Queries;
using CRMD.Domain.InventoryItems;

namespace CRMD.Application.InventoryItems.Queries
{
    public class GetAllIneventoryItemsQueryHandler : IRequestHandler<GetAllQuery<InventoryItem>, ErrorOr<List<InventoryItem>>>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;

        public GetAllIneventoryItemsQueryHandler(IInventoryItemRepository inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task<ErrorOr<List<InventoryItem>>> Handle(GetAllQuery<InventoryItem> request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _inventoryItemRepository.GetAllInventoryItemsAsync();
                if (items == null || items.Count == 0)
                    return Error.NotFound();
                return items;

            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}