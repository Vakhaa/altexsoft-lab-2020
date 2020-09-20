using System.Collections.Generic;

namespace Task3.BL.Interfaces
{
    public interface IDataManager
    {
        public List<T> Load<T>() where T : class;
        public void Save<T>(T item) where T : class;
        public void Save();
    }
}
