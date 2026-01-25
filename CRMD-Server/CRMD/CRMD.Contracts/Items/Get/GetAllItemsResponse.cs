namespace CRMD.Contracts.Items.Get;

public record GetAllItemsResponse(ErrorOr<List<ItemDto>> response);