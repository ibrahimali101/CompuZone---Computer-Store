using CompuZone.Application.Exceptions;
using CompuZone.Application.Localization;
using CompuZone.Application.Wapper;

namespace CompuZone.API.Middelware
{
    public class GlobalExceptionHandle
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandle> _logger;

        public GlobalExceptionHandle(RequestDelegate next , ILogger<GlobalExceptionHandle> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                int statusCode;
                Response<object> response;

                //var service = context.RequestServices.GetRequiredService<SharedLocalizationService>();
                switch (ex)
                {
                    case UnauthorizedAccessException:
                        statusCode = StatusCodes.Status401Unauthorized;
                        response = new Response<object> { Data = null, Message = ex.Message, IsSucceded = false };
                        break;

                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        response = new Response<object> { Data = null, Message = ex.Message, IsSucceded = false };
                        break;

                    case BusinessException:
                        statusCode = StatusCodes.Status400BadRequest;
                        response = new Response<object> { Data = null, Message = ex.Message, IsSucceded = false };
                        break;


                    case TimeoutException:
                        statusCode = StatusCodes.Status408RequestTimeout;
                        response = new Response<object> { Data = null, Message = ex.Message, IsSucceded = false };
                        break;

                    default:
                        statusCode = StatusCodes.Status500InternalServerError;
                        response = new Response<object> { Data = null, Message = ex.Message, IsSucceded = false };
                        break;
                }

                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
