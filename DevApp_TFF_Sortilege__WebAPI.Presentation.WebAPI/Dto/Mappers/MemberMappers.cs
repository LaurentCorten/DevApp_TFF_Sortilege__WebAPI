using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Mappers
{
    public static class MemberMappers
    {
        // Mapper from Model to detailed Response Dto
        public static MemberResponseDto ToResponseDto(this Member member)
        {
            return new MemberResponseDto()
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
            };
        }
    }
}
