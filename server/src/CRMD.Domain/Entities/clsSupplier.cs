namespace CRMD.Domain.Entities;

public class clsSupplier
{
    public string SupplierId { get; set; } = string.Empty;
    public string SupplierName { get; set; } = null!;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public decimal Rating { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
