using CRMD.Application.Generics.Commands;
using CRMD.Domain.InventoryItems;

namespace CRMD.Application.InventoryItems.Commands
{
    public class DeleteInventoryItemCommandHandler : IRequestHandler<DeleteCommand<InventoryItem>, ErrorOr<Deleted>>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;

        public DeleteInventoryItemCommandHandler(IInventoryItemRepository inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteCommand<InventoryItem> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();

            try
            {
                await _inventoryItemRepository.DeleteItemFromInventoryAsync(request.Id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}