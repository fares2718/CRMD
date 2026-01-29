using CRMD.Application.Generics.Commands;
using CRMD.Domain.Sections;

namespace CRMD.Application.Sections.Commands
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteCommand<Section>, ErrorOr<Deleted>>
    {
        private readonly ISectionRepository _sectionRepository;

        public DeleteSectionCommandHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteCommand<Section> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();

            try
            {
                await _sectionRepository.DeleteSectionAsync(request.Id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}