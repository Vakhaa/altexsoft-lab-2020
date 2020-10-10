using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
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
        public  async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await Task.Run(() => _context.Set<T>().Add(entity));
            return await Task.Run(()=> entity);
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            await Task.Run(()=> _context.Set<T>().Remove(entity));
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            
            return await Task.Run(() => _context.Set<T>().FirstOrDefault( item => item.Id == id));
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            if(typeof(T) == typeof(IngredientsInRecipe))
                return await Task.Run(() => _context.Set<T>().ToList());
            return await Task.Run(()=> _context.Set<T>().OrderBy(i => i.Id).ToList());
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            await Task.Run(()=> _context.Set<T>().Update(entity));
        }
    }
}
