using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HomeTask4.Infrastructure.Data
{
    public class EFRepository : IRepository
    {
        private AppDbContext _context;
        public EFRepository(AppDbContext context)
        {
            _context = context;
        }
        public  async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            return await Task.FromResult( _context.Set<T>().Add(entity).Entity);
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            await Task.Run(()=> _context.Set<T>().Remove(entity));
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            if(typeof(T) == typeof(Recipe))
            {
                await _context.Set<IngredientsInRecipe>().Include(i => i.Recipe).ThenInclude(r => r.Ingredients).ToListAsync();
                await _context.Set<IngredientsInRecipe>().Include(i => i.Ingredient).ThenInclude(r => r.IngredientsInRecipe).ToListAsync();
                await _context.Set<StepsInRecipe>().Include(s => s.Recipe).ThenInclude(r => r.StepsHowCooking).ToListAsync();
            }
            return await _context.Set<T>().OrderBy(i => i.Id).ToListAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            await Task.Run(()=> _context.Set<T>().Update(entity));
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
