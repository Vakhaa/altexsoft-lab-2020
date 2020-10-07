using System;
using HomeTask4.SharedKernel;
//using System.ComponentModel.DataAnnotations.Schema;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Подкатегории.
    /// </summary>
    //[Table("Category")]
    public class Subcategory : BaseEntity
    {
        /// <summary>
        /// Название подкатегории.
        /// </summary>
      //  [Column("Name")]
        public string Name { get; set; }
        // [Column("ParentId")]
        public int? ParentId { get; set; }
        public Subcategory() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название подкатегории.</param>
        /// <param name="parentId">Список подкатегорий.</param>
        public Subcategory(int id, string name, int parentId = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Должно быть имя подкатегории");
            }
            Name = name;
            Id = id;
            ParentId = parentId;
        }
    }
}
