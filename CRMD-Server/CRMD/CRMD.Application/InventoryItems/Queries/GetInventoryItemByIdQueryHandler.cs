using CRMD.Application.Generics.Queries;
using CRMD.Domain.InventoryItems;

namespace CRMD.Application.InventoryItems.Queries
{
    public class GetInventoryItemByIdQueryHandler : IRequestHandler<GetByIdQuery<InventoryItem>, ErrorOr<InventoryItem>>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;

        public GetInventoryItemByIdQueryHandler(IInventoryItemRepository inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task<ErrorOr<InventoryItem>> Handle(GetByIdQuery<InventoryItem> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();
            try
            {
                var item = await _inventoryItemRepository.GetInventoryItemByIdAsync(request.Id);
                if (item == null)
                    return Error.NotFound();
                return item;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}