namespace CRMD.Application.Sections.Commands
{
    public record UpdateSectionCommand(
        int SectionId,
        int CaptainId,
        short TablesCount
    ) : IRequest<ErrorOr<Updated>>;
}