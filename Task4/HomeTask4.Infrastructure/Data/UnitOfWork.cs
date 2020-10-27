using System.Threading.Tasks;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Infrastructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private AppDbContext _context;
        public IRepository Repository {get;}
        public UnitOfWork(IRepository repository, AppDbContext context)
        {
            Repository = repository;
            _context = context;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
