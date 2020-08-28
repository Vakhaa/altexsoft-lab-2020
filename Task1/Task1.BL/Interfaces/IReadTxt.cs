using System;

namespace Task1.BL.Interfaces
{   
    /// <summary>
     /// Интерфейс для чтения файлов формата txt.
     /// </summary>
    public interface IReadTxt
    {
        public String ReadTxt();
        public void CreateFile(string text);
    }
}
