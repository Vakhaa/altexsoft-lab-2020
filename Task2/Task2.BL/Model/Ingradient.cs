using System;

namespace Task2.BL.Model
{
    /// <summary>
    /// Ингредиенты
    /// </summary>
    [Serializable]
    public class Ingradient
    {
        /// <summary>
        /// Название ингредиента
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Созадние ингредиента
        /// </summary>
        /// <param name="Name">Название ингредиента</param>
        public Ingradient(string Name)
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException("Имя ингредиента не должно быть пустое.", nameof(Name));
            }
            this.Name = Name;
        }
        public override string ToString()
        {
            return "Ingradient";
        }
    }
}
