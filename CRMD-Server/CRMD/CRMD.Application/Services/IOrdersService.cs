namespace CRMD.Application.Services;

public interface IOrdersService
{
    public int PlaceAnOrder(List<int> orderItemsIds, short orderType);
}