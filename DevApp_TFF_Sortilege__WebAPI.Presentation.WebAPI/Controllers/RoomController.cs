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
        private readonly IHubContext<LobbyHub, ILobbyHub> _lobbyHub;

        public RoomController(IRoomService roomService, IHubContext<LobbyHub, ILobbyHub> lobbyHub)
        {
            _roomService = roomService;
            _lobbyHub = lobbyHub;
        }
        #endregion

        
        #region C
        [HttpPost]
        [ProducesResponseType<RoomResponseDto>(201)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> CreateNewRoom([FromBody] RoomRequestDtoNew dtoNew)
        {
            try
            {
                // Retrieve the current user's ID from the JWT token
                Guid memberId = new Guid(HttpContext.UserId());

                // Call RoomService to create the room
                Room newRoom = _roomService.CreateRoom(dtoNew.RoomName, memberId);

                // Initiate the signalR group for that room
                await _lobbyHub.Groups.AddToGroupAsync(dtoNew.ConnectionId, $"Room_{newRoom.Id}");

                // Notify all connected clients
                await _lobbyHub.Clients.Group("Lobby").RoomCreated(newRoom.ToResponseDto());

                return CreatedAtAction(nameof(GetRooms), newRoom.ToResponseDto());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }
        } 

        [HttpPost("/api/lobby")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateLobbyConnection([FromBody] string connectionId)
        {
            // Connect to Lobby group and get check ping
            await _lobbyHub.Groups.AddToGroupAsync(connectionId, "Lobby");
            await _lobbyHub.Clients.Client(connectionId).LobbyJoined();

            // Register the connection → member mapping for disconnect cleanup
            LobbyHub.RegisterConnection(connectionId, new Guid(HttpContext.UserId()));

            return NoContent();
        }
        #endregion

        // R
        [HttpGet]
        [ProducesResponseType<IEnumerable<RoomResponseDto>>(200)]
        public IActionResult GetRooms() => Ok(
            _roomService.GetAllRooms().Select(RoomMappers.ToResponseDto)            
            );

        // TODO : Rassembler les 2 routes PUT en une seule avec un l'action join ou leave en param pour REST
        #region U
        [HttpPut("join")]
        [ProducesResponseType<RoomResponseDto>(200)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> JoinRoom([FromBody]RoomRequestDtoUpdate dto)
        {
            try
            {
                // Call RoomService to join the room
                Room updatedRoom = _roomService.JoinRoom(dto.RoomId, new Guid(HttpContext.UserId()));
                RoomResponseDto roomDto = updatedRoom.ToResponseDto();

                // Join SignalR room's group
                await _lobbyHub.Groups.AddToGroupAsync(dto.ConnectionId, $"Room_{ dto.RoomId}");

                // Notify all connected clients
                await _lobbyHub.Clients.Group("Lobby").RoomUpdated(roomDto);

                return Ok(roomDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }

        }


        [HttpPut("leave")]
        [ProducesResponseType<RoomResponseDto>(200)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> LeaveRoom([FromBody] RoomRequestDtoUpdate dto)
        {
            try
            {
                // Call RoomService to join the room
                Room updatedRoom = _roomService.LeaveRoom(dto.RoomId, new Guid(HttpContext.UserId()));
                RoomResponseDto roomDto = updatedRoom.ToResponseDto();

                // Join SignalR room's group
                await _lobbyHub.Groups.RemoveFromGroupAsync(dto.ConnectionId, $"Room_{dto.RoomId}");

                // Notify all connected clients
                await _lobbyHub.Clients.Group("Lobby").RoomUpdated(roomDto);

                return Ok(updatedRoom.ToResponseDto());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }
        } 
        #endregion

        // D
        [HttpDelete("delete")]
        [ProducesResponseType(204)]
        [ProducesResponseType<BadRequest>(400)]
        public async Task<IActionResult> DeleteRoom([FromBody] RoomRequestDtoUpdate dto)
        {
            try
            {
                // Call RoomService to join the room
                bool success = _roomService.DeleteRoom(dto.RoomId, new Guid(HttpContext.UserId()));

                // Join SignalR room's group
                await _lobbyHub.Groups.RemoveFromGroupAsync(dto.ConnectionId, $"Room_{dto.RoomId}");

                // Notify all connected clients
                await _lobbyHub.Clients.Group("Lobby").RoomDeleted(dto.RoomId);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // TODO : À affiner avec en fonction de l'erreur catched
            }
        }
    }
}
