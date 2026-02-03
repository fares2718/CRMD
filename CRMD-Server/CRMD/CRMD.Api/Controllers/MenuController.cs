using CRMD.Application.DTOs;
using CRMD.Application.Generics.Commands;
using CRMD.Application.Generics.Queries;
using CRMD.Application.MenuItems.Commands;
using CRMD.Application.MenuItems.Queries;
using CRMD.Contracts.MenuItems.Post;
using CRMD.Contracts.MenuItems.Put;
using CRMD.Domain.Categories;
using CRMD.Domain.Menu;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {

        private readonly ISender _mediator;

        public MenuController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-menu-item", Name = "add-menu-item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddMenuItem(AddNewMenuItemRequest request)
        {
            var cmd = new AddNewMenuItemCommand(request.Name,
                request.Recipe,
                request.Price,
                request.CategoryId);
            var addMenuItemResult = await _mediator.Send(cmd);
            return addMenuItemResult.MatchFirst(
                created => CreatedAtRoute("add-menu-item", new AddResponse(created)),
                error => Problem(new AddResponse(error).ToString()));
        }

        [HttpDelete("delete-menu-item/{id}", Name = "delete-menu-item")]

        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var cmd = new DeleteCommand<MenuItem>(id);
            var addMenuItemResult = await _mediator.Send(cmd);
            return addMenuItemResult.MatchFirst(
                deleted => Ok(new DeleteResponse(deleted)),
                error => Problem(new DeleteResponse(error).ToString()));
        }

        [HttpGet("get-menu-categories", Name = "get-menu-categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMenuCategories()
        {
            var query = new GetAllQuery<Category>();
            var getmenucategoriesResult = await _mediator.Send(query);
            return getmenucategoriesResult.MatchFirst(
                categories => Ok(new GetAllResponse<Category>(categories)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllResponse<Category>(error)) :
                Problem(new GetAllResponse<Category>(error).ToString())
            );
        }

        [HttpGet("get-menu-items", Name = "get-menu-items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMenuItems()
        {
            var query = new GetMenuItemsQuery();
            var getMenuItemsResult = await _mediator.Send(query);
            return getMenuItemsResult.MatchFirst(
                menuItems => Ok(new GetAllResponse<MenuItemDto>(menuItems)),
                error => Problem(new GetAllResponse<MenuItemDto>(error).ToString())
            );

        }

        [HttpPut("update-recipe", Name = "update-recipe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateRecipe(UpdateRecipeRequest request)
        {
            var cmd = new UpdateRecipeCommand(request.RecipeItems);
            var updateRecipeResult = await _mediator.Send(cmd);
            return updateRecipeResult.MatchFirst(
                updated => Ok(new UpdateResponse(updated)),
                error => Problem(new UpdateResponse(error).ToString())
            );
        }

    }
}