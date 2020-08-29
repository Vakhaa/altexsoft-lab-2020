namespace Task1.BL.Interfaces
{   
    /// <summary>
     /// Интерфейс для чтения файлов формата txt.
     /// </summary>
    public interface IFileTxt
    {
        public string ReadTxt();
        public void CreateFile(string text);
        public void OpenFile(string path);
    }
}
