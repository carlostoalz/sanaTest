using System.Linq.Expressions;

namespace SanaTest.Infraestructure
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetByFilerAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetFilterFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstOrDefaultAsync();
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entity);
        Task<T> DeleteAsync(long id);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entity);
    }
}