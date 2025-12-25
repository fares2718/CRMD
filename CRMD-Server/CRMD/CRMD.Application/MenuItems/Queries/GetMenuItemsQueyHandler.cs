namespace CRMD.Application.MenuItems.Queries
{
    public class GetMenuItemsQueyHandler : IRequestHandler<GetMenuItemsQuery, ErrorOr<List<MenuItemDto>>>
    {
        private readonly IMenuRepository _menuRepository;

        public GetMenuItemsQueyHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<ErrorOr<List<MenuItemDto>>> Handle(GetMenuItemsQuery request,
         CancellationToken cancellationToken)
        {
            var menuItems = await _menuRepository.GetAllMenuItemsAsync();
            if (menuItems is null || !menuItems.Any())
            {
                return Error.NotFound();
            }
            return menuItems;
        }
    }
}