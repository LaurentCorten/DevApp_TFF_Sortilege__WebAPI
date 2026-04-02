using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Configs;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Mappers;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Request;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        #region DI
        private readonly IRoomService _roomService;
        private readonly IHubContext<RoomHub> _hubCtx;

        public RoomController(IRoomService roomService, IHubContext<RoomHub> hubCtx)
        {
            _roomService = roomService;
            _hubCtx = hubCtx;
        } 
        #endregion

        // C
        [HttpPost]
        [ProducesResponseType<RoomResponseDto>(201)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> CreateNewRoom([FromBody]RoomRequestDtoNew dtoNew)
        {
            try
            {
                // Call RoomService to create the room
                Room newRoom = _roomService.CreateRoom(dtoNew.RoomName, new Guid(HttpContext.UserId()));

                // Initiate the group for that room
                //await _hubCtx.Groups.AddToGroupAsync(connectionId, newRoom.Id);
                // TODO : à changer qd la vidéo en sera là.

                // Notify all connected clients
                await _hubCtx.Clients.All.SendAsync("ReceiveRoomsListUpdate", _roomService.GetAllRooms());               

                return CreatedAtAction(nameof(GetRooms), newRoom.ToResponseDto());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }
        }

        // R
        [HttpGet]
        [ProducesResponseType<IEnumerable<RoomResponseDto>>(200)]
        public IActionResult GetRooms() => Ok(
            _roomService.GetAllRooms().Select(RoomMappers.ToResponseDto)            
            );

        // U  // TODO : Rassembler les 2 routes PUT en une seule avec un l'action join ou leave en param pour REST
        [HttpPut("/join/{roomId}")]
        [ProducesResponseType<RoomResponseDto>(200)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> JoinRoom([FromRoute]Guid roomId)
        {
            try
            {
                // Call RoomService to join the room
                Room updatedRoom = _roomService.JoinRoom(roomId, new Guid(HttpContext.UserId()));

                // Join SignalR room's group
                //await _hubCtx.Groups.AddToGroupAsync(connectionId, roomId);
                // TODO : à changer qd la vidéo en sera là.

                // Notify all connected clients
                await _hubCtx.Clients.All.SendAsync("ReceiveRoomsListUpdate", _roomService.GetAllRooms());

                return Ok(updatedRoom.ToResponseDto());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }

        }


        [HttpPut("/leave/{roomId}")]
        [ProducesResponseType<RoomResponseDto>(200)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> LeaveRoom([FromRoute] Guid roomId)
        {
            try
            {
                // Call RoomService to join the room
                Room updatedRoom = _roomService.LeaveRoom(roomId, new Guid(HttpContext.UserId()));

                // Join SignalR room's group
                //await _hubCtx.Groups.RemoveFromGroupAsync(connectionId, roomId);
                // TODO : à changer qd la vidéo en sera là.

                // Notify all connected clients
                await _hubCtx.Clients.All.SendAsync("ReceiveRoomsListUpdate", _roomService.GetAllRooms());

                return Ok(updatedRoom.ToResponseDto());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }
        }

        // D
        [HttpDelete("/{roomId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> DeleteRoom([FromRoute]Guid roomId)
        {
            try
            {
                // Call RoomService to join the room
                bool success = _roomService.DeleteRoom(roomId, new Guid(HttpContext.UserId()));

                // Join SignalR room's group
                //await _hubCtx.Groups.RemoveFromGroupAsync(connectionId, roomId);
                // TODO : à changer qd la vidéo en sera là.

                // Notify all connected clients
                await _hubCtx.Clients.All.SendAsync("ReceiveRoomsListUpdate", _roomService.GetAllRooms());

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }
        }
    }
}
