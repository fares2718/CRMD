namespace CRMD.Contracts.Orders.Post;

public record PlaceAnOrderResponse(ErrorOr<Created> Result);