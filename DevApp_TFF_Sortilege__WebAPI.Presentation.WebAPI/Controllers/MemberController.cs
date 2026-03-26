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

        #region DI
        private readonly IMemberService _memberService;
        private readonly TokenTools _tokenTools;

        public MemberController(IMemberService memberService, TokenTools tokenTools)
        {
            _memberService = memberService;
            _tokenTools = tokenTools;
        }
        #endregion

        #region Auth
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAsync([FromBody] MemberRequestDtoReg dto)
        {

            Member memberToAdd = new Member(
                string.IsNullOrWhiteSpace(dto.Name?.Trim()) ? "Quidam" + (rnd.Next(999, 9999) * rnd.Next(999, 9999)).ToString() : dto.Name,
                dto.EmailAddress,
                dto.Password
                );

            try
            {
                Member addedMember = await _memberService.RegisterAsync(memberToAdd);

                return Ok(new { message = $"Votre compte à bien été créé {addedMember.Name} !" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LoginAsync([FromBody]MemberRequestDtoLog dto)
        {
            try
            {
                Member member = await _memberService.LoginAsync(dto.EmailAddress, dto.Password);

                string token = _tokenTools.Generate(new TokenTools.Data()
                {
                    MemberId = member.Id
                });

                return Ok(new
                {
                    Message = $"Bienvenue {member.Name} !",
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        #endregion
    }
}
