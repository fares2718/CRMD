using System;

namespace CRMD.Domain.Entities;

public class clsDiscount
{
    public int Id { get; set;  }
    public string Name { get; set; } = null!;
    public decimal Value { get; set; }
    public string Type { get; set; } = null!;
    public string Promocode { get; set; } = null!;
    public bool IsActive { get; set; }
}
