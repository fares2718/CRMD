namespace CRMD.Application.Tables.Commands
{
    public record AddTableCommand(
        int SectionId,
        int WaiterId,
        short Capacity
    ) : IRequest<ErrorOr<Created>>;
}