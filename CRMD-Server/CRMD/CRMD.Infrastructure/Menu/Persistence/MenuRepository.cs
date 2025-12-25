using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMD.Application.Common.Interfaces;
using CRMD.Domain.Menu;
using Npgsql;

namespace CRMD.Infrastructure.Menu
{
    public class MenuRepository : IMenuRepository
    {
        private readonly string _connectionString;

        public MenuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (var cmd = new NpgsqlCommand("addmenuitem", conn))
                {
                    //ToDo: Implement AddMenuItemAsync method
                }

            }
        }
    }
}