using SanaTest.BE;
using SanaTest.Domain;

namespace SanaTest.API
{
    public class ProductQuery(IProductService service)
    {
        IProductService _service { get; } = service;
        public async Task<IEnumerable<ProductDTO>> GetProducts() => await this._service.GetProducts();

    }
}
