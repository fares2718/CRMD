namespace CRMD.Contracts.Items.Delete;

public record DeleteItemResponse(ErrorOr<Deleted> response);