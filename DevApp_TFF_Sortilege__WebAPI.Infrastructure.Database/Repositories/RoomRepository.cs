using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public List<Room> _rooms { get; } = []; //? Dictionary ou Enum mieux ?


        // C
        public Room CreateRoom(Room newRoom)
        {
            _rooms.Add(newRoom);
            return newRoom;
        }


        // R
        public List<Room> GetAllRooms() => _rooms.ToList();

        public Room? GetRoomByRoomId(Guid roomId) => _rooms.SingleOrDefault(r => r.Id == roomId);

        public Room? GetRoomByUserId(Guid userId) => _rooms.SingleOrDefault(r => r.CreatorId == userId || r.GuestId == userId);

        public bool CheckRoomNameExists(string roomName) => _rooms.Any(r => r.Name == roomName);
        

        // U
        public Room UpdateRoom(Room updatedRoom)
        {
            _rooms.RemoveAll(r => r.Id == updatedRoom.Id);
            _rooms.Add(updatedRoom);
            return updatedRoom;
        }


        // D
        public bool DeleteRoom(Guid roomId)
        {
            _rooms.RemoveAll(r => r.Id == roomId);
            return true;
        }
    }
}
