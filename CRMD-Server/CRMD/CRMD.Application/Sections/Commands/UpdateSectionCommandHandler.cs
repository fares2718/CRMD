
using CRMD.Domain.Sections;

namespace CRMD.Application.Sections.Commands
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, ErrorOr<Updated>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public UpdateSectionCommandHandler(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }






        public async Task<ErrorOr<Updated>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            if (request.SectionId < 1 || request.CaptainId < 1 || request.TablesCount < 1)
                return Error.Validation();

            try
            {
                var newSectionData = _mapper.Map<Section>(request);
                await _sectionRepository.UpdateSectionAsync(newSectionData);
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}