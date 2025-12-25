using AutoMapper;
using CRMD.Application.DTOs;
using CRMD.Application.MenuItems.Commands;
using CRMD.Application.Orders.Commands;
using CRMD.Domain.Menu;
using CRMD.Domain.Orders;

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
    }
}