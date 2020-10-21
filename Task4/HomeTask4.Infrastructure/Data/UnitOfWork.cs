using System;
using System.Threading.Tasks;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Infrastructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private IRepository _repository;
        
        public IRepository Repository
        {
            get
            {
                return _repository;
            }
        }
        public UnitOfWork(IRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveAsync();
        }
    }
}
