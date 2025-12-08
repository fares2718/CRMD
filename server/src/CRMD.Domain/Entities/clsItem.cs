using System;

namespace CRMD.Domain.Entities;

public class clsItem
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Unit { get; set; } = null!;
    public decimal MinLevel { get; set; }
    public decimal Cost { get; set; }
}
