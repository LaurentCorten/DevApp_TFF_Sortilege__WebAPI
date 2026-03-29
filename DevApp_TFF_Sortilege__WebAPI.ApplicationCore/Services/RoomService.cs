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


        // U
        public Room JoinRoom(Guid roomId, Guid memberId)
        {
            // Call RoomRepo to check if the room exists and if the user isn't already in it
            Room? existingRoom = _roomRepo.GetRoomByRoomId(roomId);
            if (existingRoom is null)
                throw new ArgumentException($"Lobby {roomId} introuvable !");
            Room? insideRoom = _roomRepo.GetRoomByUserId(memberId);
            if (insideRoom is not null)
                throw new ArgumentException (
                    (existingRoom == insideRoom) ? 
                    "Vous êtes déjà dans ce lobby !" :
                    $"Vous êtes déjà inscrit dans le lobby {insideRoom.Id} : {insideRoom.Name} !"
                    );
            // Gard full room
            if (existingRoom.GuestId is not null)
                throw new InvalidOperationException("Ce lobby est déjà plein !");

            // Since ok, update the room and send to repo
            Room updatedRoom = new Room(roomId, existingRoom.Name, existingRoom.CreatorId, existingRoom.TimeStamp, memberId);
            return _roomRepo.UpdateRoom(updatedRoom);
        }


        public Room LeaveRoom(Guid roomId, Guid memberId)
        {
            // Gard room exists and user is in it ?
            Room? existingRoom = _roomRepo.GetRoomByRoomId(roomId);
            if (existingRoom is null)
                throw new ArgumentException($"Lobby {roomId} introuvable !");
            Room? insideRoom = _roomRepo.GetRoomByUserId(memberId);
            if (insideRoom is null)
                throw new ArgumentException($"L'utilisateur {memberId} n'est dans aucun lobby !");
            if (existingRoom != insideRoom)
                throw new ArgumentException($"L'utilisateur {memberId} n'est pas dans le lobby {roomId} mais dans le lobby {insideRoom.Id} !");
            // Gard empty room
            if (memberId == existingRoom.CreatorId && existingRoom.GuestId is null)
                throw new ArgumentException("Un lobby ne peut pas être laissé vide, il faut le supprimer !");

            // Since gard ok
            Room updatedRoom;
            if (memberId == existingRoom.CreatorId)
            {
                // Set the "Guest" as "Creator" so single-member rooms always have the same format and it's easier to handle 
                updatedRoom = new Room(roomId, existingRoom.Name, new Guid(existingRoom.GuestId.ToString()!), existingRoom.TimeStamp); // Seem to be forced to parse the Guid? to cast it in Guid even if we're sure it isn't null thx to gard tests...
            } 
            else
            {
                updatedRoom = new Room(roomId, existingRoom.Name, existingRoom.CreatorId, existingRoom.TimeStamp);
            }

            return _roomRepo.UpdateRoom(updatedRoom);

        }


        // D
        public bool DeleteRoom(Guid roomId, Guid requesterId)
        {
            // Gard room exists and request comes from rooom's "creator"
            Room? existingRoom = _roomRepo.GetRoomByRoomId(roomId);
            if (existingRoom is null)
                throw new ArgumentException($"Lobby {roomId} introuvable !");
            if (existingRoom.CreatorId != requesterId)
                throw new ArgumentException("L'utilisateur à l'origine de la requête n'est pas l'utilisateur responsable de ce lobby !");

            // Since gard ok
            return _roomRepo.DeleteRoom(roomId);
        }
    }
}
