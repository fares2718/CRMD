using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMD.Application.MenuItems.Commands;
using CRMD.Application.MenuItems.Queries;
using CRMD.Contracts.MenuItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost(Name = "add-menu-item")]
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

        [HttpGet(Name = "get-menu-items")]
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

    }
}