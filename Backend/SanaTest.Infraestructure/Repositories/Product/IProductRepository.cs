using SanaTest.BE;

namespace SanaTest.Infraestructure
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
