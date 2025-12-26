namespace CRMD.Application.Map;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PlaceAnOrderCommand, Order>();
        CreateMap<OrderItemsDto, OrderItems>();
        CreateMap<Order, OrderDto>();
        CreateMap<AddNewMenuItemCommand, MenuItem>();
        CreateMap<RecipeDto, Recipe>();
        CreateMap<RecipeItemDto, RecipeItem>();
        CreateMap<AddNewEmployeeCommand, Employee>();
    }
}