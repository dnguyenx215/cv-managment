using CVManagementAPI.Models;
using System.Linq.Expressions;

namespace CVManagementAPI.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> ToListAsync();
        Task<List<T>> SearchAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteManyAsync(List<T> entities);

        Task<bool> SelectAsync(Expression<Func<T, bool>> predicate);
    }
}