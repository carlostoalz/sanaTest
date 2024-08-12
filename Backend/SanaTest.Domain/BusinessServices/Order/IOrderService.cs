using SanaTest.BE;

namespace SanaTest.Domain
{
    public interface IOrderService
    {
        Task CreateOrder(OrderDTO order);
    }
}
