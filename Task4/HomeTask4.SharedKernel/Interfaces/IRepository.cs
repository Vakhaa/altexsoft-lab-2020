using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task AddRangeAsync<T>(List<T> entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        Task<T> GetWithIncludeEntityAsync<T>(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
        Task<IEnumerable<T>> GetWithIncludeListAsync<T>(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
        Task<IEnumerable<T>> GetWithIncludeListAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
    }
}
