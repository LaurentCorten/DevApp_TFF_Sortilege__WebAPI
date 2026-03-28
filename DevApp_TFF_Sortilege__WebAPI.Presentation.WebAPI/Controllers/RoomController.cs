using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Configs;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IHubContext<RoomHub> _hubCtx;

        public RoomController(IRoomService roomService, IHubContext<RoomHub> hubCtx)
        {
            _roomService = roomService;
            _hubCtx = hubCtx;
        }

        [HttpGet]
        public IActionResult GetRooms() => Ok(_roomService.GetAllRooms());

        [HttpPost]
        public async Task<IActionResult> CreateNewRoom(string roomName)
        {
            // Call RoomService to create the room
            Room newRoom = _roomService.CreateRoom(roomName, new Guid(HttpContext.UserId()));

            // Initiate the group for that room
            //await _hubCtx.Groups.AddToGroupAsync(connectionId, newRoom.Id);
            // TODO : à changer qd la vidéo en sera là.

            // Notify all connected clients
            await _hubCtx.Clients.All.SendAsync("ReceiveRoomsListUpdate", _roomService.GetAllRooms());

            return Ok(newRoom);
        }

        [HttpPut("/join/{roomId}")]
        public async Task<IActionResult> JoinRoom([FromRoute]Guid roomId)
        {
            // Call RoomService to join the room
            Room updatedRoom = _roomService.JoinRoom(roomId, new Guid(HttpContext.UserId()));

            // Join SignalR room's group
            //await _hubCtx.Groups.AddToGroupAsync(connectionId, roomId);
            // TODO : à changer qd la vidéo en sera là.

            // Notify all connected clients
            await _hubCtx.Clients.All.SendAsync("ReceiveRoomsListUpdate", _roomService.GetAllRooms());

            return Ok(updatedRoom);

        }

        [HttpPut("/leave/{roomId}")]
        public async Task<IActionResult> LeaveRoom([FromRoute]Guid roomId)
        {
            throw new NotImplementedException();
        }
    }
}
