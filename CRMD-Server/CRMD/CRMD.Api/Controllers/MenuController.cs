using CRMD.Application.MenuItems.Commands;
using CRMD.Application.MenuItems.Queries;
using CRMD.Contracts.MenuItems;

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
            if (string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0 ||
             request.Recipe == null ||
             request.CategoryId <= 0)
                return BadRequest("Invalid request");

            var cmd = new AddNewMenuItemCommand(request.Name,
                request.Recipe,
                request.Price,
                request.CategoryId);
            var addMenuItemResult = await _mediator.Send(cmd);
            return addMenuItemResult.MatchFirst(
                created => CreatedAtRoute("add-menu-item", created),
                error => Problem(error.Description));
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
                menuItems => Ok(new GetMenuItemsResponse(menuItems)),
                error => Problem(error.Description, error.Code)
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
                updated => Ok(new { Message = "Recipe updated successfully." }),
                error => Problem(error.Description, error.Code)
            );
        }

    }
}