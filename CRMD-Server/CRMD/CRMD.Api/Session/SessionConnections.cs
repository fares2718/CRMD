using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMD.Api.Models;

namespace CRMD.Api.Session
{
    public class SessionConnections
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
        public ConcurrentDictionary<string, UserConnection> Connections => _connections;
    }
}