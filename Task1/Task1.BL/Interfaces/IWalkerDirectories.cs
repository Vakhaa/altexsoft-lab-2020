namespace Task1.BL.Interfaces
{
    public interface IWalkerDirectories
    {
        /// <summary>
        /// Метод для смены диска.
        /// </summary>
        public void ChangeDisk();
        /// <summary>
        /// Поиск директории.
        /// </summary>
        /// <param name="str">Название директории</param>
        public void SearchDirectories(string str);
        /// <summary>
        /// Устанавливает директорию, по заданому пути.
        /// </summary>
        /// <param name="path">Местоположение директории.</param>
        public void SetDirectories(string path);
    }
}
