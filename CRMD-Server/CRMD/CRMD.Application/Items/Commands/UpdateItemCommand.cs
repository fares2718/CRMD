namespace CRMD.Application.Items.Commands;

public record UpdateItemCommand(
    int ItemId,
    decimal Price
) : IRequest<ErrorOr<Updated>>;