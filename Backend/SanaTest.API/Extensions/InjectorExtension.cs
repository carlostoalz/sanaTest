using System.Diagnostics.CodeAnalysis;
using SanaTest.BE;
using SanaTest.Infraestructure;
using SanaTest.Domain;

namespace SanaTest.API
{
    [ExcludeFromCodeCoverage]
    internal static class InjectorExtension
    {
        internal static void AddDependencys(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<GlobalAppSettings>();

            #region Domain
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            #endregion            

            #region Infraestructure
            services.AddScoped<IDapperRepository, DapperRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            #endregion
        }
    }
}