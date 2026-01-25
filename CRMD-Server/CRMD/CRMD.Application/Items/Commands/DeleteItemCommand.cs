namespace CRMD.Application.Items.Commands;

public record DeleteItemCommand(int Id) : IRequest<ErrorOr<Deleted>>;