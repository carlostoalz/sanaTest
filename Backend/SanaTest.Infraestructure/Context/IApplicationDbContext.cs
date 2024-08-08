using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace SanaTest.Infraestructure
{
    public interface IApplicationDbContext
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void SetModified(object entity);
        void SetDetached(object entity);
    }
}
