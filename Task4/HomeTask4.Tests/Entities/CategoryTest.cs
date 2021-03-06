﻿using HomeTask4.Core.Entities;
using Xunit;

namespace HomeTask4.Tests.Entities
{
    public class CategoryTest
    {
        [Fact]
        public void Constructor_Category_IfDataCorrect_CreateEntity()
        {
            // Arrange
            var parentId = 1; 
            var childCategoryName = "expected";
            // Act
            // Run method which should be tested

            var result = new Category(childCategoryName, parentId);
            // Assert
            Assert.Equal(childCategoryName, result.Name);
            Assert.Equal(parentId, result.ParentId);
        }
    }
}
