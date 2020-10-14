using System;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{ 
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Category() { }
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
