using CRMD.Application.Departments.Commands;
using CRMD.Application.InventoryItems.Commands;
using CRMD.Application.Items.Commands;
using CRMD.Application.PerchaseInvoices;
using CRMD.Application.Sections.Commands;
using CRMD.Application.Tables.Commands;
using CRMD.Application.Users.Commands;
using CRMD.Domain.Departments;
using CRMD.Domain.InventoryItems;
using CRMD.Domain.Items;
using CRMD.Domain.PerchaseInvoices;
using CRMD.Domain.Sections;
using CRMD.Domain.Tables;
using CRMD.Domain.Users;

namespace CRMD.Application.Map;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddDepartmentCommand, Department>();
        CreateMap<UpdateDepartmentCommand, Department>();
        CreateMap<AddNewEmployeeCommand, Employee>();
        CreateMap<UpdateEmployeeSalaryCommand, Employee>();
        CreateMap<AddInventoryItemCommand, InventoryItem>();
        CreateMap<UpdateInventoryItemCommand, InventoryItem>();
        CreateMap<AddItemCommand, Item>();
        CreateMap<UpdateItemCommand, Item>();
        CreateMap<AddNewMenuItemCommand, MenuItem>();
        CreateMap<UpdateRecipeCommand, Recipe>();
        CreateMap<AddOrderCommand, Order>();
        CreateMap<AddPerchaseInvoiceCommand, PerchaseInvoice>();
        CreateMap<AddSectionCommand, Section>();
        CreateMap<AddTableCommand, Table>();
        CreateMap<AddUserCommand, User>();
    }
}