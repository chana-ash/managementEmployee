namespace Manage.Api.MiddleWares
{
    public static class ShabbatMiddlewareExtensions
    {
        public static IApplicationBuilder UseShabbat(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShabbatMiddleware>();
        }
    }
}
