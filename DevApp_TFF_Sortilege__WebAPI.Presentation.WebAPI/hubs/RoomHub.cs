using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Hubs
{
    //public interface IRoomHub
    //{

    //}

    public class RoomHub : Hub/*<IRoomHub>*/
    {

        // Just to get a confirmation on connection
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("UserConnected");
        }

        // This is the emmitter fct sending "real-time" room List
        public async Task SendRoomsList(List<Room> rooms)
        {
            await Clients.All.SendAsync("ReceiveRoomsListUpdated", rooms);
        }

        // This is the emmitter fct sending "real-time" creation of newroom
        public async Task SendNewRoom(RoomResponseDto room)
        {
            await Clients.All.SendAsync("ReceiveNewRoom", room);
        }
    }
}
