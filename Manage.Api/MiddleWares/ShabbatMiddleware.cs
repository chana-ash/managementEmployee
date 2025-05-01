namespace Manage.Api.MiddleWares
{
    public class ShabbatMiddleware
    {
        private readonly RequestDelegate _next;

        public ShabbatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // בדיקת היום הנוכחי
            var now = DateTime.Now;

            // אם שבת (DayOfWeek.Saturday)
            if (now.DayOfWeek == DayOfWeek.Saturday)
          
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("השירות אינו פעיל בשבת");
                return;
            }

            // אם לא שבת - ממשיכים ל-Request הבא בצנרת
            await _next(context);
        }
    }
}

