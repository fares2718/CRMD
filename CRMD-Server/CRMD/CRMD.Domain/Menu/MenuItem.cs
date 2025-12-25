using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Domain.Menu
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public short CategoryId { get; set; }
    }
}