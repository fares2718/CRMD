
namespace CRMD.Application.Items.Commands
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, ErrorOr<Deleted>>
    {
        private readonly IItemRepository _itemRepository;

        public DeleteItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation("Invalid Data");

            try
            {
                await _itemRepository.DeleteItemAsync(request.Id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}