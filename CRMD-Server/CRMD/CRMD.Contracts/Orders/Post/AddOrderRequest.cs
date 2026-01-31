namespace CRMD.Contracts.Orders.Post;

public record AddOrderRequest(List<OrderItemsDto> OrderItemsDtos,
    EnOrderType OrderType,
    int TableId,
    int CaptainId,
    decimal TotalAmount,
    DateTime CreatedAt);