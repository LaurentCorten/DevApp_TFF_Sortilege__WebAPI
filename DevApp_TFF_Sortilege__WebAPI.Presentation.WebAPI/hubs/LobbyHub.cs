using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Mappers;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Hubs
{
    public interface ILobbyHub
    {
        Task LobbyJoined();
        Task RoomCreated(RoomResponseDto newRoom);
        Task RoomDeleted(Guid roomId);
        Task RoomUpdated(RoomResponseDto updatedRoom);
    }

    public class LobbyHub : Hub<ILobbyHub>
    {
        #region DI
        private readonly IRoomService _roomService;

        public LobbyHub(IRoomService roomService)
        {
            _roomService = roomService;
        }
        #endregion

        #region Garbage collector" to clean rooms if users just disconnect without leaving or deleting their room
        // In-memory map of ConnectionId → MemberId
        // Static so it's shared across all hub instances
        private static readonly ConcurrentDictionary<string, Guid> _connections = new();

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string connectionId = Context.ConnectionId;

            // Find the member associated with this connection
            if (_connections.TryRemove(connectionId, out Guid memberId))
            {
                // Check if this member was in a room
                Room? room = _roomService.GetRoomByMemberId(memberId);

                if (room != null)
                {
                    // Apply the same logic as LeaveRoom/DeleteRoom
                    if (room.CreatorId == memberId && room.GuestId == null)
                    {
                        // Cas 1 : creator alone → delete
                        _roomService.DeleteRoom(room.Id, memberId);
                        await Clients.Group("Lobby").RoomDeleted(room.Id);
                    }
                    else
                    {
                        // Cas 2 & 3 : leave and transfer if needed
                        Room updatedRoom = _roomService.LeaveRoom(room.Id, memberId);
                        await Clients.Group("Lobby").RoomUpdated(updatedRoom.ToResponseDto());
                        await Clients.Group($"Room_{room.Id}").RoomUpdated(updatedRoom.ToResponseDto());
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public static void RegisterConnection(string connectionId, Guid memberId)
        {
            _connections[connectionId] = memberId;
        }
        #endregion
    }
}
