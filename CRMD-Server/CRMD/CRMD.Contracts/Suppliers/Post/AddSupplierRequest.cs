namespace CRMD.Contracts.Suppliers.Post;

public record AddSupplierRequest(
    string name,
    string[] phones,
    string address
);