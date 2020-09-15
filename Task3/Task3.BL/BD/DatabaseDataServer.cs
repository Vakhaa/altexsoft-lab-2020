using System.Collections.Generic;
using System.Linq;
using Task2.BL.DAL;

namespace Task2.BL.BD
{
    public class DatabaseDataServer
    {
        public List<T> Load<T>() where T : class
        {
            using (var db = new BookRecipesContext())
            {
                return db.Set<T>().ToList();
            }
        }

        public void Save<T>(List<T> item) where T : class
        {
            using (var db = new BookRecipesContext())
            {
                db.Set<T>().AddRange(item);
                db.SaveChanges();
            }
        }
    }
}
