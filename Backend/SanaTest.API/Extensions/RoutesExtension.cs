namespace SanaTest.API
{
    internal static class RoutesExtension
    {
        internal static void UseUserRoutes(this WebApplication app)
        {
            app.ProductRoutes();
        }
    }
}
