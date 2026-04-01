namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response
{
    public class RoomResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid CreatorId { get; set; } = default!;
        public Guid? GuestId { get; set; } = default!;
        //public List<Member>? SpectatorMembers { get; private set; } = [];
        public DateTime CreationDate { get; set; }
    }
}
