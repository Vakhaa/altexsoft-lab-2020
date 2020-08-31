using System.Collections.Generic;
using Task2.BL.Controler;
using Task2.BL.Model;

namespace Task2.BL.Interfaces
{
    public interface IIngradientRepository
    {
        public GenericRepository<List<Ingradient>, Ingradient> IngradientRepository { get; }
        public void Save(UnitOfWork uow);
        public void Dispose();
    }
}
