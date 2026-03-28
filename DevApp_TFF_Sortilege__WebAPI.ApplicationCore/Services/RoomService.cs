using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services
{
    public class RoomService : IRoomService
    {
        // DI
        private readonly RoomManager _manager;

        public RoomService(RoomManager manager)
        {
            _manager = manager;
        }

        // C
        public Room CreateRoom(Room room)
        {
            // Gards
            Room? checkUser = GetRoomByUserId(room.CreatorMember.Id); //? Changer pour ne pas dépendre da l'autre fonction ?
            if (checkUser is not null)
                throw new ArgumentException("Cet utilisateur est déjà dans un autre lobby !"); // TODO: custom exception
            
            bool checkName = _manager.Rooms.Any(r => r.Name == room.Name);
            if (checkName)
                throw new ArgumentException("Ce nom de lobby est déjà pris !"); // TODO: custom exception

            // If ok
            Room newRoom = new(Guid.NewGuid(), room.Name, room.CreatorMember, room.InitTimeStamp);
            _manager.Rooms.Add(newRoom);
            return newRoom;
        }

        // R
        public List<Room> GetAllRooms() => _manager.Rooms.ToList();

        public Room? GetRoomByRoomId(Guid roomId) => _manager.Rooms.SingleOrDefault(r => r.Id == roomId);

        public Room? GetRoomByUserId(Guid userId)
        {
            return _manager.Rooms.SingleOrDefault(r => r.CreatorMember.Id == userId || r.GuestMember?.Id == userId);
        }

        // U
        public Room JoinRoom(Guid roomId, Guid memberId)
        {
            throw new NotImplementedException();
        }

        public Room LeaveRoom(Guid roomId, Guid memberId)
        {
            throw new NotImplementedException();
        }

        // D
        public bool DeleteRoom(Guid roomId)
        {
            throw new NotImplementedException();
        }
    }
}
