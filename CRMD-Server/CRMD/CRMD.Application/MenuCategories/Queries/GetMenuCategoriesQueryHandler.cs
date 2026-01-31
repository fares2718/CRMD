using CRMD.Application.Generics.Queries;
using CRMD.Domain.Categories;

namespace CRMD.Application.MenuCategories.Queries
{
    public class GetMenuCategoriesQueryHandler : IRequestHandler<GetAllQuery<Category>, ErrorOr<List<Category>>>
    {
        private readonly IMenuRepository _menuRepository;

        public GetMenuCategoriesQueryHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<ErrorOr<List<Category>>> Handle(GetAllQuery<Category> request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _menuRepository.GetMenuCategoriesAsync();
                if (categories == null || categories.Count == 0)
                    return Error.NotFound();

                return categories;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}