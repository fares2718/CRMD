
namespace CRMD.Application.Items.Queries
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, ErrorOr<List<ItemDto>>>
    {
        private readonly IItemRepository _itemRepository;

        public GetAllItemsQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ErrorOr<List<ItemDto>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _itemRepository.GetAllItemsAsync();
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