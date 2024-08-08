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
            #endregion            

            #region Infraestructure
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IDapperRepository, DapperRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion
        }
    }
}