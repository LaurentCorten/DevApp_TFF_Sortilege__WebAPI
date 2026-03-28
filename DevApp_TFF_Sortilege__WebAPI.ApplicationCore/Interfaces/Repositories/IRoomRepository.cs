using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        // C
        public Room CreateRoom(Room newRoom);

        // R
        public List<Room> GetAllRooms();
        public Room? GetRoomByRoomId(Guid roomId);
        public Room? GetRoomByUserId(Guid userId);
        public bool CheckRoomNameExists(string roomName);

        // U
        public Room UpdateRoom(Room updatedRoom);

        // D
        public bool DeleteRoom(Guid roomId);
    }
}
