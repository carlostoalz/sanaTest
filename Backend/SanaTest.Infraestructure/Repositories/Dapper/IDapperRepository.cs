using System.Data;
using Dapper;

namespace SanaTest.Infraestructure
{
    public interface IDapperRepository
    {
        Task QueryVoidAsync(string sql, DynamicParameters? dynamicParameters);
        Task<IEnumerable<XOutput>> QueryAsync<XOutput>(string sql, DynamicParameters? dynamicParameters);
        Task<XOutput> QueryFirstOrDefaultAsync<XOutput>(string sql, DynamicParameters? dynamicParameters);
        Task<XOutput> QueryMultipleAsync<XOutput>(string sql, DynamicParameters? dynamicParameters) where XOutput : new();
    }
}