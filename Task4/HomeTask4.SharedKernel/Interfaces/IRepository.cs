using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
        Task<T> GetByNameAsync<T>(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        Task<bool> IsExistsAsync<T>(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
        Task<IEnumerable<T>> GetWithIncludeAsync<T>(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
        Task<IEnumerable<T>> GetWithIncludeAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity;
    }
}
