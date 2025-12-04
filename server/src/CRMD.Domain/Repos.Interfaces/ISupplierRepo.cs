using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface ISupplierRepo
{
    public Task<int> AddSupplierAsync(clsSupplier supplier);
        public Task<bool> DeleteSupplierAsync(string supplierId);
    public Task<List<clsSupplier>> GetAllSuppliersAsync();
    public Task<clsSupplier?> GetSupplierByIdAsync(int supplierId);
    public Task<bool> UpdateSupplierPhoneAsync(string SupplierId,string newPhone);
}
