using CRMD.Application.DTOs;
using CRMD.Application.Generics.Commands;
using CRMD.Application.Generics.Queries;
using CRMD.Application.Sections.Commands;
using CRMD.Contracts.Sections.Post;
using CRMD.Contracts.Sections.Put;
using ErrorOr;
using static System.Collections.Specialized.BitVector32;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : ControllerBase
    {
        private readonly ISender _mediator;

        public SectionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-section", Name = "add-section")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddSection(AddSectionRequest request)
        {
            var cmd = new AddSectionCommand(request.CaptainId, request.TablesCount);
            var addSectionResult = await _mediator.Send(cmd);
            return addSectionResult.MatchFirst(
                created => CreatedAtRoute("add-section", new AddResponse(created)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) :
                Problem(new AddResponse(error).ToString())
            );
        }

        [HttpDelete("delete-section/{id}", Name = "delete-section")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var cmd = new DeleteCommand<Section>(id);
            var deleteSectionResult = await _mediator.Send(cmd);
            return deleteSectionResult.MatchFirst(
                deleted => Ok(new DeleteResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteResponse(error))
                : Problem(new DeleteResponse(error).ToString())
            );
        }

        [HttpGet("get-all", Name = "get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> GetAllDepartment()
        {
            var query = new GetAllQuery<SectionDto>();
            var getSectionsResult = await _mediator.Send(query);
            return getSectionsResult.MatchFirst(
                Sections => Ok(new GetAllResponse<SectionDto>(Sections)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllResponse<SectionDto>(error))
                : Problem(new GetAllResponse<SectionDto>(error).ToString())
            );
        }

        [HttpGet("get-Section/{id}", Name = "get-Section")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            var query = new GetByIdQuery<SectionDto>(Id);
            var getSectionResult = await _mediator.Send(query);
            return getSectionResult.MatchFirst(
                Section => Ok(new GetByIdResponse<SectionDto>(Section)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetByIdResponse<SectionDto>(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetByIdResponse<SectionDto>(error))
                : Problem(new GetByIdResponse<SectionDto>(error).ToString())
            );
        }

        [HttpPut("update-Section", Name = "update-Section")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateDepartment(UpdateSectionRequest request)
        {
            var cmd = new UpdateSectionCommand(request.SectionId, request.CaptainId, request.TablesCount);
            var updateSectionResult = await _mediator.Send(cmd);
            return updateSectionResult.MatchFirst(
                updated => Ok(new UpdateResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateResponse(error))
                : Problem(new UpdateResponse(error).ToString())
            );
        }
    }
}