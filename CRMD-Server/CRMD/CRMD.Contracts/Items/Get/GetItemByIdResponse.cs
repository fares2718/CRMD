namespace CRMD.Contracts.Items.Get;

public record GetItemByIdResponse(ErrorOr<ItemDto> response);