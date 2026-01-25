
using CRMD.Domain.Items;

namespace CRMD.Application.Items.Commands
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ErrorOr<Updated>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            if (request.ItemId < 1 || request.Price < 0)
                return Error.Validation("Invalid Data");
            try
            {
                var newItemData = _mapper.Map<Item>(request);
                await _itemRepository.UpdateItemAsync(newItemData);
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}