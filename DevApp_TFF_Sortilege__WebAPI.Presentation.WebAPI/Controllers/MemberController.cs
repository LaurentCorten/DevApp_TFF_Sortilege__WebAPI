using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Configs;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Mappers;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Request;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Response;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Token;
using Microsoft.AspNetCore.Authorization;
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

        // C
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

                MemberResponseDto memberDto = member.ToResponseDto();

                return Ok(new // TODO: Faire un LoginResponseDto propre
                {
                    Message = $"Bienvenue {memberDto.Name} !",
                    Member = memberDto,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        // R
        [HttpGet]
        [Authorize] // Needed to refresh memberState w. accessToken
        [ProducesResponseType<MemberResponseDto>(200)]
        public async Task<IActionResult> GetMemberByTokenAsync()
        {
            Guid memberId = new Guid(HttpContext.UserId());

            try
            {
                Member member = await _memberService.GetMemberByIdAsync(memberId);

                return Ok(member.ToResponseDto());
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }
    }
}
