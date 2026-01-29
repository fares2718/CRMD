using CRMD.Application.Generics.Queries;
using CRMD.Domain.InventoryItems;

namespace CRMD.Application.Sections.Queries
{
    public class GetISectionByIdQueryHandler : IRequestHandler<GetByIdQuery<SectionDto>, ErrorOr<SectionDto>>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetISectionByIdQueryHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<ErrorOr<SectionDto>> Handle(GetByIdQuery<SectionDto> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();
            try
            {
                var section = await _sectionRepository.GetSectionByIdAsync(request.Id);
                if (section == null)
                    return Error.NotFound();
                return section;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}