using SanaTest.BE;

namespace SanaTest.Infraestructure
{
    public class ProductRepository(IDapperRepository dapperRepository, GlobalAppSettings globalAppSettings) : IProductRepository
    {
        private IDapperRepository _dapperRepository { get; } = dapperRepository;
        private GlobalAppSettings _globalAppSettings { get; } = globalAppSettings;
        public async Task<IEnumerable<ProductDTO>> GetProducts() => await this._dapperRepository.QueryAsync<ProductDTO>(this._globalAppSettings.Settings.Procedures.GetProducts, null);
    }
}
