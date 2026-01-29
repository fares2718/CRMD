namespace CRMD.Application.Tables.Commands
{
    public record UpdateTableCommand(
        int TableId,
        int WaiterId,
        short Capacity
    ) : IRequest<ErrorOr<Updated>>;
}