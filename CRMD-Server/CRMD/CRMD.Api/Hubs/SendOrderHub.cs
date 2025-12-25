using CRMD.Api.Models;
using CRMD.Api.Session;
using CRMD.Application.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace CRMD.Api.Hubs
{
    public class SendOrderHub : Hub
    {
        private readonly SessionConnections _sessionConnections;

        public SendOrderHub(SessionConnections sessionConnections)
        {
            _sessionConnections = sessionConnections;
        }

        public async Task JoinRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,
             conn.ConnectionRoom);

            _sessionConnections.Connections[Context.ConnectionId] = conn;

            await Clients.Group(conn.ConnectionRoom).SendAsync("JoinedRoom",
             conn.Username, $"Joined room {conn.ConnectionRoom}");
        }

        public async Task SendOrderMessage(OrderDto order)
        {
            if (_sessionConnections.Connections.TryGetValue(Context.ConnectionId, out UserConnection? conn))
            {
                await Clients.Group(conn.ConnectionRoom).SendAsync("ReceiveOrder", conn.Username,
                 order);
            }
        }
    }
}