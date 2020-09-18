using System.Collections.Generic;
using System.Linq;
using Task2.BL.DAL;
using Task3.BL.Interfaces;

namespace Task2.BL.BD
{
    public class DatabaseDataServer :IDataManager
    {
        public List<T> Load<T>() where T : class
        {
            using (var db = new BookRecipesContext())
            {
                return db.Set<T>().ToList();
            }
        }

        public void Save<T>(T item) where T : class
        {
            using (var db = new BookRecipesContext())
            {
                db.Set<T>().Add(item);  
                db.SaveChanges();
            }
        }

        public void Save()
        {
            using (var db = new BookRecipesContext())
            {
                db.SaveChanges();
            }
        }
    }
}
