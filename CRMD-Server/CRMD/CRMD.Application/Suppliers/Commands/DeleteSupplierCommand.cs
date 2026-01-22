namespace CRMD.Application.Suppliers.Commands
{
    public record DeleteSupplierCommand(int supplierId) : IRequest<ErrorOr<Deleted>>;
}