using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;


namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Mappers
{
    public static class RoomMappers
    {
        // Mapper from Model to detailed Response Dto
        public static RoomResponseDtoDetails ToResponseDtoDetails (this Room room)
        {
            return new RoomResponseDtoDetails()
            {
                Id = room.Id,
                Name = room.Name,
                CreatorId = room.CreatorId,
                GuestId = room.GuestId,
                CreationDate = room.TimeStamp
            };
        }
    }
}
