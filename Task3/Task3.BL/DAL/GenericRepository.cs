using System;
using System.Collections.Generic;
using Task3.BL.Interfaces;

namespace Task2.BL.Controler
{
    public class GenericRepository<T>:IDisposable where T : class
    {
        private List<T> _context;
        private readonly IDataManager _dataManager;
        private bool disposedValue;

        public GenericRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _context = _dataManager.Load<T>();
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
            _dataManager.Save(item);
        }
        public virtual void Save()
        {
            _dataManager.Save();
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
