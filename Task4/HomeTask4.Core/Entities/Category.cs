using System;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{ 
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Category() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название категории.</param>
        /// <param name="parentId">Id подкатегории.</param>
        public Category( string name, int parentId=0)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Должно быть имя категории");
            }   
            if(parentId!=0)
            ParentId = parentId;
            Name = name;
        }
    }
}
