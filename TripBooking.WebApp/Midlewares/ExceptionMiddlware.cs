using TripBooking.Core.Exceptions;

namespace TripBooking.WebApp.Midlewares
{
    internal class ExceptionMiddlware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new Error(ex.GetType().Name, ex.Message));
            }
        }

        private record Error(string Code, string Message);
    }
}
