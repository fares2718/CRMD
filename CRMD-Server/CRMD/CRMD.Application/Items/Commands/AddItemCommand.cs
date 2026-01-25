namespace CRMD.Application.Items.Commands;

public record AddItemCommand(
    int CategoryId,
    decimal Price,
    string Name
) : IRequest<ErrorOr<Created>>;