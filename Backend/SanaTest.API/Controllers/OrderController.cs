using SanaTest.BE;
using SanaTest.Domain;

namespace SanaTest.API
{
    public static class OrderController
    {
        public static void OrderRoutes(this WebApplication app)
        {
            var group = app.MapGroup("api/orders").WithTags("Orders");
            group.MapPost("/", async (IOrderService service, OrderDTO order) => await service.CreateOrder(order));
        }
    }
}
