using Dapper;
using SanaTest.BE;
using System.Data;
using System.Text.Json;

namespace SanaTest.Infraestructure
{
    public class OrderRepository(IDapperRepository dapperRepository, GlobalAppSettings globalAppSettings) : IOrderRepository
    {
        private IDapperRepository _dapperRepository { get; } = dapperRepository;
        private GlobalAppSettings _globalAppSettings {  get; } = globalAppSettings;
        public async Task CreateOrder(OrderDTO order)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@order_info", JsonSerializer.Serialize(order) ,DbType.String, ParameterDirection.Input);
            await this._dapperRepository.QueryVoidAsync(_globalAppSettings.Settings.Procedures.CreateOrder, dynamicParameters);
        }
    }
}
