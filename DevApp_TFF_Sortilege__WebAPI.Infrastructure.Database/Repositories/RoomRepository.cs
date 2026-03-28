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

        public Room? GetRoomByRoomId(Guid roomId)
        {
            throw new NotImplementedException();
        }

        public Room? GetRoomByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool CheckRoomNameExists(string roomName)
        {
            return _rooms.Any(r => r.Name == roomName);
        }

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
            throw new NotImplementedException();
        }
    }
}
