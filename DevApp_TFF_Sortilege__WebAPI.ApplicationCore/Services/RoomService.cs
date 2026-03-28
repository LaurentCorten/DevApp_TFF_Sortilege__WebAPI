using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services
{
    public class RoomService : IRoomService
    {
        // DI
        private readonly IRoomRepository _roomRepo;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepo = roomRepository;
        }

        // C
        public Room CreateRoom(string roomName, Guid creatorId)
        {
            // Gards
            Room? checkUser = _roomRepo.GetRoomByUserId(creatorId);
            if (checkUser is not null)
                throw new ArgumentException("Cet utilisateur est déjà dans un autre lobby !"); // TODO: custom exception
                        
            if (_roomRepo.CheckRoomNameExists(roomName))
                throw new ArgumentException("Ce nom de lobby est déjà pris !"); // TODO: custom exception

            // If ok
            Room newRoom = new(Guid.NewGuid(), roomName, creatorId, DateTime.Now);
            return _roomRepo.CreateRoom(newRoom);
        }

        // R
        public List<Room> GetAllRooms() => _roomRepo.GetAllRooms();

        //public Room? GetRoomByRoomId(Guid roomId) => _manager.Rooms.SingleOrDefault(r => r.Id == roomId);

        //public Room? GetRoomByUserId(Guid userId)
        //{
        //    return _manager.Rooms.SingleOrDefault(r => r.CreatorId == userId || r.GuestId == userId);
        //}

        // U
        public Room JoinRoom(Guid roomId, Guid memberId)
        {
            // Call RoomRepo to check if the room exists and if the user isn't already in it
            Room? existingRoom = _roomRepo.GetRoomByRoomId(roomId);
            if (existingRoom is null)
                throw new ArgumentException($"Lobby {roomId} introuvablle !");
            Room? insideRoom = _roomRepo.GetRoomByUserId(memberId);
            if (insideRoom is not null)
                throw new ArgumentException (
                    (existingRoom == insideRoom) ? 
                    "Vous êtes déjà dans ce lobby !" :
                    $"Vous êtes déjà inscrit dans le lobby {insideRoom.Id} : {insideRoom.Name} !"
                    );

            // Since ok, update the room and send to repo
            Room updatedRoom = new Room(roomId, existingRoom.Name, existingRoom.CreatorId, existingRoom.TimeStamp, memberId);
            return _roomRepo.UpdateRoom(updatedRoom);
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
