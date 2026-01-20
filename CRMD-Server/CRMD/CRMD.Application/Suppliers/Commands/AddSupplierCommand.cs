namespace CRMD.Application.Suppliers.Commands;

public record AddSupplierCommand(
    string name,
    string[] phones,
    string address
) : IRequest<ErrorOr<Created>>;