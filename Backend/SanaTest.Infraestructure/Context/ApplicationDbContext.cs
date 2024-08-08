using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace SanaTest.Infraestructure
{
    public class ApplicationDbContext: IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public IDbConnection Connection => Database.GetDbConnection();
        public virtual void SetDetached(object entity) => Entry(entity).State = EntityState.Detached;
        public virtual void SetModified(object entity) => Entry(entity).State = EntityState.Modified;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
