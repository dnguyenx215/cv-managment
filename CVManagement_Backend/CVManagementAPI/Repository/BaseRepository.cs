using CVManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CVManagementAPI.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly CVManagementDbContext _context;

        public BaseRepository(CVManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int> InsertAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }



        public async Task<int> DeleteAsync(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteManyAsync(List<T> entities)
        {
            var attachedEntities = new List<T>();

            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    attachedEntities.Add(entity);
                }

                entry.State = EntityState.Deleted;
            }

            _context.Set<T>().AttachRange(attachedEntities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateManyAsync(List<T> entitiesToUpdate)
        {
            var attachedEntities = new List<T>();

            foreach (var entity in entitiesToUpdate)
            {
                var entry = _context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    attachedEntities.Add(entity);
                }

                entry.State = EntityState.Modified;
            }
            _context.Set<T>().UpdateRange(attachedEntities);
            // Save the changes
            return await _context.SaveChangesAsync();
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _context.Set<T>().OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<T?> FindAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task<bool> SelectAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<List<T>> SearchAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}