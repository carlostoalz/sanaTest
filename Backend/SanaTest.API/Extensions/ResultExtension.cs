using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using SanaTest.BE;

namespace SanaTest.API
{
    internal static class ResultExtension
    {
        internal static IResult ResultResponse<T>(this IResultExtensions resultExtensions, T data, string message = null, bool isSuccess = true)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions, nameof(resultExtensions));
            return new Result<T>(data, message, isSuccess);
        }
        class Result<T> : IResult
        {
            public Result(T data, string message = null, bool isSuccess = true)
            {
                this.Data = data;
                this.Message = message;
                this.IsSuccess = isSuccess;
            }

            public T Data { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }

            public Task ExecuteAsync(HttpContext httpContext)
            {
                string content = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    Converters = { new DecimalJsonConverter() }
                });
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(content);
                switch (httpContext.Request.Method)
                {
                    case "GET":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        break;
                    case "POST":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Created;
                        break;
                    case "PUT":
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;
                    default:
                        break;
                }
                return httpContext.Response.WriteAsync(content);
            }
        }
    }
}