namespace CRMD.Application.Items.Queries;

public record GetItemByIdQuery(int Id) : IRequest<ErrorOr<ItemDto>>;