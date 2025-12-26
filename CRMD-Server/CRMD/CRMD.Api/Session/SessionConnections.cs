using System.Collections.Concurrent;
using CRMD.Api.Models;

namespace CRMD.Api.Session
{
    public class SessionConnections
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
        public ConcurrentDictionary<string, UserConnection> Connections => _connections;
    }
}