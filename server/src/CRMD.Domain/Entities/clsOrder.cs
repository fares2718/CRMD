using System;

namespace CRMD.Domain.Entities;

public class clsOrder
{
    public int Id  { get; set; }
    public int TableId { get; set; }
    public string WaiterId { get; set; }=null!;
    public short Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAt { get; set; }
}
