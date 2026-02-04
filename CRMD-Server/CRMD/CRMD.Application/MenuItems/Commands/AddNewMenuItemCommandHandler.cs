namespace CRMD.Application.MenuItems.Commands
{
    public class AddNewMenuItemCommandHandler :
    IRequestHandler<AddNewMenuItemCommand, ErrorOr<Created>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public AddNewMenuItemCommandHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddNewMenuItemCommand request,
        CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0 ||
             request.Ingredients == null || request.Ingredients.Count == 0 || request.CategoryId <= 0)
                return Error.Validation();
            var menuItem = _mapper.Map<MenuItem>(request);
            await _menuRepository.AddMenuItemAsync(menuItem);
            return Result.Created;
        }
    }
}