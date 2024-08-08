using SanaTest.BE;

namespace SanaTest.Domain
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
