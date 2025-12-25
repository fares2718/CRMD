using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMD.Domain.Menu;

namespace CRMD.Application.Common.Interfaces
{
    public interface IMenuRepository
    {
        public Task AddMenuItemAsync(MenuItem menuItem);
    }
}