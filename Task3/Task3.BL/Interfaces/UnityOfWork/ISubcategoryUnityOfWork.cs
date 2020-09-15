using System;
using System.Collections.Generic;
using System.Text;
using Task2.BL.Controler;
using Task2.BL.Model;

namespace Task2.BL.Interfaces
{
    public interface ISubcategoryUnityOfWork:IDisposable
    {
        GenericRepository<Subcategory> SubcategoryRepository { get; }
        void Save();
    }
}
