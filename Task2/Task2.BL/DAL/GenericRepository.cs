using System;
using System.Collections.Generic;

namespace Task2.BL.Controler
{
    public class GenericRepository<T,T1>:IDisposable where T : List<T1>
    {
        private T _context;
        private string _fileName;
        private JSONManager _jsonManager;
        private bool disposedValue;

        public GenericRepository( string fileName)
        {
            _jsonManager = new JSONManager();
            _context = (T)_jsonManager.DeserialezeFile<T1>(fileName);

            _fileName =fileName;
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
            _jsonManager.Save(_context, _fileName);
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
                _fileName = null;
                _jsonManager = null;
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
