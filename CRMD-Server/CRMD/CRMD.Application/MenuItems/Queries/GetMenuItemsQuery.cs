namespace CRMD.Application.MenuItems.Queries;

public record GetMenuItemsQuery() : IRequest<ErrorOr<List<MenuItemDto>>>;