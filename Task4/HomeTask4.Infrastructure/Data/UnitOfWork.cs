using System;
using System.Threading.Tasks;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Infrastructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        public IRepository Repository {get;}
        public UnitOfWork(IRepository repository)
        {
            Repository = repository;
        }
    }
}
