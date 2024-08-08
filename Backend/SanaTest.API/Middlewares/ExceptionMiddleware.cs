using System.Net;
using System.Text.Json;
using SanaTest.BE;

namespace SanaTest.API
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await GetResult(context, ex, HttpStatusCode.Conflict);
            }
        }
        private async Task GetResult(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var route = context.GetRouteData();
            string controllerName = context.Request.Path.ToString();
            string actionName = context.Request.Method.ToString();
            context.Response.Clear();
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = @"application/json";
            var error = CreateErrorResponse(exception, context, controllerName, actionName);
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
        private ErrorResponse CreateErrorResponse(Exception exception, HttpContext context, string controllerName, string actionName)
        {
            return new()
            {
                Message = GetMessageException(exception),
                ControllerName = controllerName,
                ActionName = actionName,
                ErrorCode = context.Response.StatusCode,
                RequestIP = GetRemoteIPAdress(context)
            };
        }
        private string GetMessageException(Exception exception, string message = "")
        {
            message += string.IsNullOrEmpty(message) ? $" Exception: {exception.Message}" : $"{exception.Message}. ";
            message += exception.InnerException != null ? $". Inner Exception={exception.InnerException.Message}." : "";
            return message;
        }
        private string GetRemoteIPAdress(HttpContext context)
        {
            try
            {
                return context.Request.Headers["IpAddress"].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}