using System;
using CRMD.Domain.Repos.Interfaces;

namespace CRMD.Domain.Entities;

public class clsDailyConsumption
{
    public int Id {get;set;}
    public int MenuItemId {get;set;}
    public DateTime ConsumeDate {get;set;}= DateTime.UtcNow;
    public decimal Quantity  {get;set;}
    public string Notes {get;set;}=string.Empty;
}
