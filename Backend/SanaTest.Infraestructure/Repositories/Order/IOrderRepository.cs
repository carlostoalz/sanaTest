using SanaTest.BE;

namespace SanaTest.Infraestructure
{
    public interface IOrderRepository
    {
        Task CreateOrder(OrderDTO order);
    }
}
