namespace CRMD.Domain.Suppliers
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string[] Phones { get; set; } = null!;
        public string Address { get; set; } = string.Empty;
    }
}