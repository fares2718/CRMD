using System;

namespace CRMD.Domain.Entities;

public class clsMenuItem
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
}
