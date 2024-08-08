using System.Diagnostics.CodeAnalysis;

namespace SanaTest.API
{
    [ExcludeFromCodeCoverage]
    public static class MiddlewareExtension
    {
        public static void UseUserMiddlewares(this WebApplication app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}