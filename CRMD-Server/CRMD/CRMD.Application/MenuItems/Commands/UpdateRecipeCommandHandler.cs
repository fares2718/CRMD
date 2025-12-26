namespace CRMD.Application.MenuItems.Commands
{
    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, ErrorOr<Updated>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public UpdateRecipeCommandHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateRecipeCommand request,
         CancellationToken cancellationToken)
        {
            if (request.RecipeItems == null || !request.RecipeItems.Any())
                return Error.Validation("400", "Recipe items cannot be null or empty.");

            var recipeItems = _mapper.Map<List<RecipeItem>>(request.RecipeItems);
            await _menuRepository.UpdateRecipeAsync(recipeItems);
            return Result.Updated;
        }
    }
}