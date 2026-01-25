
namespace CRMD.Application.Items.Queries
{
    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ErrorOr<ItemDto>>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ErrorOr<ItemDto>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();
            try
            {
                var item = await _itemRepository.GetItemByIdAsync(request.Id);
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