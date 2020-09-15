using System;
using System.Collections.Generic;
using Task2.BL.BD;

namespace Task2.BL.Controler
{
    public class GenericRepository<T>:IDisposable where T : class
    {
        private List<T> _context;
        private readonly DatabaseDataServer _databaseDataServer;
        private bool disposedValue;

        public GenericRepository( )
        {
            _databaseDataServer = new DatabaseDataServer();
            _context = _databaseDataServer.Load<T>();
        }
        public virtual List<T> Get()
        {
            return _context;
        }
        public virtual T GetByID(int id)
        {
            return _context[id];
        }
        public virtual void Insert(T item)
        {
            _context.Add(item);
        }
        public virtual void Save()
        {
            _databaseDataServer.Save(_context);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Save();
                }

                _context = null;
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
