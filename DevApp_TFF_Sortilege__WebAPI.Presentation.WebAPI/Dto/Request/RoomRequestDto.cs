using System.ComponentModel.DataAnnotations;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Request
{
    public class RoomRequestDtoNew
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string RoomName { get; set; } = default!;

        [Required]
        // SignalR connection ID of the requesting client,
        // used to add them to the appropriate SignalR groups
        public string ConnectionId { get; set; } = default!;
    }
}
