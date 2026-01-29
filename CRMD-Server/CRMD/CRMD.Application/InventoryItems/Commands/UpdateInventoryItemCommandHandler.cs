
using CRMD.Domain.InventoryItems;

namespace CRMD.Application.InventoryItems.Commands
{
    public class UpdateInventoryItemCommandHandler : IRequestHandler<UpdateInventoryItemCommand, ErrorOr<Updated>>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IMapper _mapper;

        public UpdateInventoryItemCommandHandler(IInventoryItemRepository inventoryItemRepository, IMapper mapper)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
        }


        public async Task<ErrorOr<Updated>> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            if (request.ItemId < 1 || request.Quantity < 0)
                return Error.Validation();

            try
            {
                var newInventoryItemData = _mapper.Map<InventoryItem>(request);
                await _inventoryItemRepository.UpdateInventoryItem(newInventoryItemData);
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}