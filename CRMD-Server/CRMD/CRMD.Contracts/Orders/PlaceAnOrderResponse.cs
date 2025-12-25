namespace CRMD.Contracts.Orders;

public record PlaceAnOrderResponse(ErrorOr<Created> Result);