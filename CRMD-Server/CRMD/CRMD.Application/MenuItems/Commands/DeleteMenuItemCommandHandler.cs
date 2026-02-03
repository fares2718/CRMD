using CRMD.Application.Generics.Commands;

namespace CRMD.Application.MenuItems.Commands
{
    public class DeleteMenuItemCommandHandler : IRequestHandler<DeleteCommand<MenuItem>, ErrorOr<Deleted>>
    {
        private readonly IMenuRepository _menuRepository;

        public DeleteMenuItemCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteCommand<MenuItem> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();
            try
            {
                await _menuRepository.DeleteMenuItemAsync(request.Id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}