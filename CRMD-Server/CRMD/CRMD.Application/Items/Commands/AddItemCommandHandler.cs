
using CRMD.Domain.Items;

namespace CRMD.Application.Items.Commands
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, ErrorOr<Created>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public AddItemCommandHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            if (request.CategoryId < 1 || request.Price < 0 || string.IsNullOrEmpty(request.Name))
                return Error.Validation("Invalid Data");
            try
            {
                var item = _mapper.Map<Item>(request);
                await _itemRepository.AddItemAsync(item);
                return Result.Created;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}