using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Infrastructure.Data
{
    public class Repository : IRepository
    {
        private AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public  Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return Task.Run(() => entity);
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            await Task.Run(()=> _context.Set<T>().Remove(entity)).ConfigureAwait(true);
        }

        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            
            return Task.Run(() => _context.Set<T>().
            First( item => item.Id == id));
        }

        public Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
           return Task.Run(()=> _context.Set<T>().ToList());
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            /*var temp = _context.Set<T>().First(item => item.Id == entity.Id);
            temp = entity;*/
            //_context.Set<T>().Update(temp);
            await Task.Run(()=> _context.Set<T>().Update(entity)).ConfigureAwait(true);
        }
    }
}
