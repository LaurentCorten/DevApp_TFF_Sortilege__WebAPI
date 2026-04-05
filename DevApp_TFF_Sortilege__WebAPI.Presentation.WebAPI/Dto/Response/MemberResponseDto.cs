namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response
{
    public class MemberResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
