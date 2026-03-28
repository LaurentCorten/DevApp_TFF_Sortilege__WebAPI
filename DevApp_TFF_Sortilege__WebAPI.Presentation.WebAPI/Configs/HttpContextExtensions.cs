namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Configs
{
    public static class HttpContextExtensions
    {
        public static string UserId(this HttpContext ctx)
        {
            return ctx.User.Claims.FirstOrDefault(c => c.Type == "nameidentifier")!.Value; // TODO : Fonctionne pas, à débugger !!!
        }
    }
}
