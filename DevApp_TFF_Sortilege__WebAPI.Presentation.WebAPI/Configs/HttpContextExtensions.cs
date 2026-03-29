using System.Security.Claims;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Configs
{
    public static class HttpContextExtensions
    {
        public static string UserId(this HttpContext ctx)
        {
            return ctx.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        }
    }
}
