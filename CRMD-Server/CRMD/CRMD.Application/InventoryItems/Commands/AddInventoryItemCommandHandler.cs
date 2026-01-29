
using CRMD.Domain.InventoryItems;

namespace CRMD.Application.InventoryItems.Commands
{
    public class AddInventoryItemCommandHandler : IRequestHandler<AddInventoryItemCommand, ErrorOr<Success>>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IMapper _mapper;

        public AddInventoryItemCommandHandler(IInventoryItemRepository inventoryItemRepository, IMapper mapper)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Success>> Handle(AddInventoryItemCommand request, CancellationToken cancellationToken)
        {
            if (request.ItemId < 1 || request.Quantity < 0 || request.MinLevel < 1)
                return Error.Validation();

            var item = _mapper.Map<InventoryItem>(request);
            try
            {
                await _inventoryItemRepository.AddItemToInventoryAsync(item);
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}