namespace CRMD.Application.Items.Queries;

public record GetAllItemsQuery() : IRequest<ErrorOr<List<ItemDto>>>;