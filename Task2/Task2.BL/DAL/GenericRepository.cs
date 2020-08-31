using System.Collections.Generic;

namespace Task2.BL.Controler
{
    public class GenericRepository<T,T1> where T : List<T1>
    {
        internal T _context;
        internal string FILE_NAME;

        public GenericRepository( string fileName)
        {
            _context = (T)JSONManager.DeserialezeFile<T1>(fileName);

            FILE_NAME =fileName;
        }

        public virtual T Get()
        {
            return _context;
        }

        public virtual T1 GetByID(int id)
        {
            return _context[id];
        }
        public virtual void Insert(T1 item)
        {
            _context.Add(item);
        }
        public virtual void Save()
        {
            JSONManager.Save(_context, FILE_NAME);
        }
    }
}
