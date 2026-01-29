
using CRMD.Domain.InventoryItems;
using CRMD.Domain.Sections;

namespace CRMD.Application.Sections.Commands
{
    public class AddSectionCommandHandler : IRequestHandler<AddSectionCommand, ErrorOr<Created>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public AddSectionCommandHandler(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }





        public async Task<ErrorOr<Created>> Handle(AddSectionCommand request, CancellationToken cancellationToken)
        {
            if (request.CaptainId < 1 || request.TablesCount < 1)
                return Error.Validation();

            var section = _mapper.Map<Section>(request);
            try
            {
                await _sectionRepository.AddSectionAsync(section);
                return Result.Created;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}