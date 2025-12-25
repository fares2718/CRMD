namespace CRMD.Contracts.Orders;

public record PlaceAnOrderRequest(List<OrderItemsDto> OrderItemsDtos,
    EnOrderType OrderType,
    int TableId,
    int CaptainId,
    decimal TotalAmount,
    DateTime CreatedAt);