using CVManagementAPI.Helpers.Page;
using System.Linq.Expressions;

namespace CVManagementAPI.Services
{
    public interface IBaseService<TEntity, TDto>
    {
        Task<List<TDto>> ToListAsync();
        Task<List<TDto>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TDto> FindAsync(int id);
        Task<int> InsertAsync(TDto obj);
        Task<int> UpdateAsync(int id, TDto obj);
        Task<int> DeleteAsync(int id);
        Task<PageResult<TEntity>> GetAllPagination(Pagination? pagination);
    }
}