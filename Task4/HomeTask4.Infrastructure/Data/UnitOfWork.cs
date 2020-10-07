using System.Threading.Tasks;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Infrastructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private AppDbContext _context;
        private IRepository _repository;
        
        public IRepository Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new Repository(_context);
                }
                return _repository;
            }
        }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
