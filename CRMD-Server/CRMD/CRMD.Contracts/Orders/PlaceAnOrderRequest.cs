namespace CRMD.Contracts.Orders;

public record PlaceAnOrderRequest(List<int> OrderItemsIds , EnOrderType OrderType);