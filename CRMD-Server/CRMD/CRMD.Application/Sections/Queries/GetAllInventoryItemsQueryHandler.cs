using CRMD.Application.Generics.Queries;

namespace CRMD.Application.Sections.Queries
{
    public class GetAllSectionsQueryHandler : IRequestHandler<GetAllQuery<SectionDto>, ErrorOr<List<SectionDto>>>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetAllSectionsQueryHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<ErrorOr<List<SectionDto>>> Handle(GetAllQuery<SectionDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var sections = await _sectionRepository.GetSectionsAsync();
                if (sections == null || sections.Count == 0)
                    return Error.NotFound();
                return sections;

            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}