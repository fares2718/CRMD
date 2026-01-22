using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMD.Application.Suppliers.Commands;
using CRMD.Contracts.Suppliers.Delete;
using CRMD.Contracts.Suppliers.Post;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {

        private readonly ISender _mediator;

        public SuppliersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-supplier", Name = "add-supplier")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddSupplier(AddSupplierRequest request)
        {
            if (string.IsNullOrEmpty(request.name) || string.IsNullOrEmpty(request.address) ||
            request.phones.Length == 0)
                return BadRequest("Invalid data");

            var cmd = new AddSupplierCommand
            (
                request.name,
                request.phones,
                request.address
            );
            var addSupplierResult = await _mediator.Send(cmd);

            return addSupplierResult.MatchFirst(
                created => CreatedAtRoute("add-supplier", new AddSupplierResponse(created)),
                error => Problem(error.Description)
            );
        }

        [HttpDelete("delete-supplier/{supplierId}", Name = "delete-supplier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {
            var cmd = new DeleteSupplierCommand(supplierId);

            var deleteSupplierResult = await _mediator.Send(cmd);
            return deleteSupplierResult.MatchFirst(
                deleted => Ok(new DeleteSupplierResponse(deleted)),
                error => Problem(new DeleteSupplierResponse(error).ToString())
            );
        }

    }
}