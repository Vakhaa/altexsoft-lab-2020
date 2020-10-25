using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
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

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            await _context.Set<IngredientsInRecipe>().Include(i => i.Recipe).ThenInclude(r => r.Ingredients)
                .Include(i => i.Ingredient).ThenInclude(r => r.IngredientsInRecipe).LoadAsync();

            await _context.Set<StepsInRecipe>().Include(s => s.Recipe).ThenInclude(r => r.StepsHowCooking).LoadAsync();

            return await _context.Set<T>().OrderBy(i => i.Id).ToListAsync();
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
        }
    }
}
