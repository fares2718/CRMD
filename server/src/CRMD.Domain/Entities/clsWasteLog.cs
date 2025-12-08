using System;

namespace CRMD.Domain.Entities;

public class clsWasteLog
{
    public int Id {get;set;}
    public decimal Quantity {get; set;}
    public DateTime LoggedAt {get;set;}
}
