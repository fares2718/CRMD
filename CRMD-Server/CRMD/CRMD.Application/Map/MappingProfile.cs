using CRMD.Application.PerchaseInvoices;
using CRMD.Application.Suppliers.Commands;
using CRMD.Domain.PerchaseInvoices;
using CRMD.Domain.Suppliers;

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
        CreateMap<AddPerchaseInvoiceCommand, PerchaseInvoice>();
        CreateMap<AddSupplierCommand, Supplier>();
    }
}