using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Request;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Token;
using Microsoft.AspNetCore.Mvc;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        public Random rnd = new Random();

        // DI
        private readonly IMemberService _memberService;
        private readonly TokenTools _tokenTools;

        public MemberController(IMemberService memberService, TokenTools tokenTools)
        {
            _memberService = memberService;
            _tokenTools = tokenTools;
        }

        [HttpPost("Register")]
        [ProducesResponseType(200)]
        public IActionResult Register(MemberRequestDto newMember)
        {
            Member memberToAdd = new Member(
                newMember.Name ?? "User"+(rnd.Next(999,9999)*rnd.Next(999,9999)).ToString(),
                newMember.EmailAddress,
                newMember.Password
                );

            Member addedMember = _memberService.Register(memberToAdd);

            return Ok(new {message = $"Votre compte à bien été créé {addedMember.Name} !"});
        }
    }
}
