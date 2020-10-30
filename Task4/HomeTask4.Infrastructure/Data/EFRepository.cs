using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Infrastructure.Data
{
    public class EFRepository : IRepository
    {
        private AppDbContext _context;
        public EFRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            var result = _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
        }

        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return _context.Set<T>().FirstOrDefaultAsync(item => item.Id == id);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
        }
        public async Task<IEnumerable<T>> GetWithIncludeAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity
        {
          return Include(includeProperties).AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetWithIncludeAsync<T>(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties) where T:BaseEntity
        {
            var query = Include(includeProperties);
            return query.Where(predicate).AsEnumerable();
        }

        private IQueryable<T> Include<T>(params Expression<Func<T, object>>[] includeProperties) where T: BaseEntity
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
