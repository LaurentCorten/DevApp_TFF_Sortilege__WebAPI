using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services
{
    public interface IRoomService
    {
        // C
        public Room CreateRoom(Room room);

        // R
        public List<Room> GetAllRooms();
        public Room? GetRoomByRoomId(Guid roomId);
        public Room? GetRoomByUserId(Guid userId);

        // U
        public Room JoinRoom(Guid roomId, Guid memberId);
        public Room LeaveRoom(Guid roomId, Guid memberId);

        // D
        public bool DeleteRoom(Guid roomId);

    }
}
