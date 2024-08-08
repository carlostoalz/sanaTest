using SanaTest.Domain;

namespace SanaTest.API
{
    internal static class ProductController
    {
        internal static void ProductRoutes(this WebApplication app)
        {
            var group = app.MapGroup("api/Product").WithTags("Products");
            group.MapGet("/", async (IProductService service) => Results.Extensions.ResultResponse(await service.GetProducts()));
        }
    }
}
