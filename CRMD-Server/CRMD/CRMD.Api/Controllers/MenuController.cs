using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            return StatusCode(StatusCodes.Status501NotImplemented);
        }
    }
}