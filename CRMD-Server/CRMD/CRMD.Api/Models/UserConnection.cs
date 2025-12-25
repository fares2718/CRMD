using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Api.Models
{
    public class UserConnection
    {
        public string Username { get; set; } = string.Empty;
        public string ConnectionRoom { get; set; } = string.Empty;
    }
}