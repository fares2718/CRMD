namespace CRMD.Contracts.Orders;

public record PlaceAnOrderRequest(int[] OrderItemsIds , EnOrderType OrderType);