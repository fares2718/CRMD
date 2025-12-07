using System;

namespace CRMD.Domain.Entities;

public class clsTable
{
    public int Id { get; set;}
    public string Name { get; set; } = null!;
    public string WaiterId { get; set; } = null!;
    public short Status { get; set; }
}
