using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Hubs
{
    public interface IRoomHub
    {

    }

    public class RoomHub : Hub<IRoomHub>
    {

    }
}
