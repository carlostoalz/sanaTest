using SanaTest.BE;
using SanaTest.Infraestructure;

namespace SanaTest.Domain
{
    public class OrderService(IOrderRepository repository) : IOrderService
    {
        private IOrderRepository _repository { get; } = repository;
        public async Task CreateOrder(OrderDTO order) => await this._repository.CreateOrder(order);
    }
}
