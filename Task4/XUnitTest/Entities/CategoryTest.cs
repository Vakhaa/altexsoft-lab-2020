using System;
using HomeTask4.Core.Entities;
using Xunit;

namespace XUnitTest.Entities
{
    public class CategoryTest
    {
        [Fact]
        public void CTOR_Category()
        {
            // Arrange
            var rnd = new Random();
            var parentId = rnd.Next(); 
            var childCategoryName = Guid.NewGuid().ToString();

            // Act
            // Run method which should be tested

            var newChildEntity = new Category(childCategoryName, parentId);
            // Assert
            Assert.Equal(childCategoryName, newChildEntity.Name);
            Assert.Equal(parentId, newChildEntity.ParentId);
        }
    }
}
