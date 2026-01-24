using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Application.DTOs
{
    public class ItemDto
    {
        int ItemId { get; set; }
        string Category { get; set; } = string.Empty;
        decimal Price { get; set; }
        string Name = string.Empty;
    }
}