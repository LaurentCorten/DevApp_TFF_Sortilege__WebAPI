using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Hubs
{
    //public interface IRoomHub
    //{

    //}

    public class RoomHub : Hub/*<IRoomHub>*/
    {

        // This is the emmitter fct sending "real-time" room List
        public async Task SendRoomsListUpdated(List<Room> rooms)
        {
            await Clients.All.SendAsync("ReceiveRoomsListUpdated", rooms);
        }
    }
}
