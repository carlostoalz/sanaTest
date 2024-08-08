using System.Collections;
using System.Data;
using System.Reflection;
using SanaTest.BE;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SanaTest.Infraestructure
{
    public class DapperRepository : IDapperRepository
    {
        public DapperRepository(GlobalAppSettings global) => this.Connection = new(global.Settings.DbConnection);
        private SqlConnection Connection { get; set; }
        public async Task<IEnumerable<XOutput>> QueryAsync<XOutput>(string sql, DynamicParameters? dynamicParameters) => await this.Connection.QueryAsync<XOutput>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
        public async Task<XOutput> QueryFirstOrDefaultAsync<XOutput>(string sql, DynamicParameters? dynamicParameters) => await this.Connection.QueryFirstOrDefaultAsync<XOutput>(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
        public async Task<XOutput> QueryMultipleAsync<XOutput>(string sql, DynamicParameters? dynamicParameters)
            where XOutput : new()
        {
            XOutput response = new();
            var grid = await this.Connection.QueryMultipleAsync(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
            if (grid != null)
                foreach (var prop in response.GetType().GetProperties())
                {
                    if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                    {
                        MethodInfo method = typeof(SqlMapper.GridReader).GetMethods().FirstOrDefault(x => x.Name.Equals("Read", StringComparison.OrdinalIgnoreCase) && x.IsGenericMethod && x.GetParameters().Length == 1).MakeGenericMethod(prop.PropertyType.GetGenericArguments());
                        var result = method.Invoke(grid, new object[] { true });
                        response.GetType().GetProperty(prop.Name).SetValue(response, result);
                    }
                    else
                    {
                        MethodInfo method = typeof(SqlMapper.GridReader).GetMethods().FirstOrDefault(x => x.Name.Equals("Read", StringComparison.OrdinalIgnoreCase) && x.IsGenericMethod && x.GetParameters().Length == 1).MakeGenericMethod(prop.PropertyType);
                        var result = method.Invoke(grid, new object[] { true });
                        response.GetType().GetProperty(prop.Name).SetValue(response, ((IEnumerable<object>)result).FirstOrDefault());
                    }
                }
            return response;
        }
        public async Task QueryVoidAsync(string sql, DynamicParameters? dynamicParameters) => await this.Connection.ExecuteAsync(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
    }
}