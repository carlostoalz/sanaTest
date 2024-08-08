using SanaTest.BE;
using SanaTest.Infraestructure;

namespace SanaTest.Domain
{
    public class ProductService(IProductRepository repository) : IProductService
    {
        private IProductRepository _repository { get; } = repository;
        public async Task<IEnumerable<ProductDTO>> GetProducts() => await this._repository.GetProducts();
    }
}
