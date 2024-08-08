using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SanaTest.Infraestructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _DbSet { get; set; }
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _DbSet = _dbContext.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _DbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            try
            {
                await _DbSet.AddRangeAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return entity;
        }
        public async Task<T> DeleteAsync(long id)
        {
            if (await _DbSet.FindAsync(id) is T entity)
            {
                _DbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            return null;
        }
        public async Task DeleteAsync(T entity)
        {
            _DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _DbSet.ToListAsync(cancellationToken);
        public async Task<IEnumerable<T>> GetByFilerAsync(Expression<Func<T, bool>> predicate) => await _DbSet.Where(predicate).ToListAsync();
        public async Task<T> GetFilterFirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _DbSet.Where(predicate).FirstOrDefaultAsync();
        public async Task<T> GetFirstOrDefaultAsync() => await _DbSet.FirstOrDefaultAsync();
        public async Task<T> UpdateAsync(T entity)
        {
            _DbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entity)
        {
            foreach (var item in entity)
                _dbContext.SetDetached(item);
            _dbContext.UpdateRange(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            _dbContext.RemoveRange(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}