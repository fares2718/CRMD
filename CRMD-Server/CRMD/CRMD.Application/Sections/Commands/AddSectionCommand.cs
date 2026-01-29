namespace CRMD.Application.Sections.Commands
{
    public record AddSectionCommand(
        int CaptainId,
        short TablesCount
    ) : IRequest<ErrorOr<Created>>;
}