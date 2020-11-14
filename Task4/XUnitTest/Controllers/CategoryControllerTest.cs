using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace XUnitTest.Controllers
{
    public class CategoryControllerTest
    {
        Mock<IUnitOfWork> _unitOfWorkMock; // Create mock object for IUnitOfWork
        Mock<IRepository> _repositoryMock;  // Create mock object for IRepository
        CategoryController _controller; // Create controller which should be tested
        Category _expectedCategory;
        List<Category> _expectedListCategory;
        public CategoryControllerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IRepository>();

            _expectedCategory = new Category
            {
                Id = 1,
                Name = "expected",
                Parent = new Category
                {
                    Id = 2,
                    Name = "expected"
                }
            };

            _expectedListCategory = new List<Category>()
            {
                _expectedCategory
            };
            // Simulate "AddAsync" method from "IRepository" to return new test entity
            _repositoryMock.Setup(o => o.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync((Category x) => x);

            // Simulate "Repository" property to return prevously created mock object for IRepository
            _unitOfWorkMock.SetupGet(o => o.Repository)
                .Returns(_repositoryMock.Object);

            _unitOfWorkMock.Setup(o => o.SaveAsync());

            _controller = new CategoryController(_unitOfWorkMock.Object);
        }
        [Fact]
        public async Task GetCategories_IfIsItems_ReturnItems()
        {
            //Arrange
            MakeMockWithIncludeListForRepository();

            // Act
            // Run method which should be tested
            var result = await _controller.GetCategoriesAsync();

            // Assert
            Assert.Same(_expectedListCategory, result);

        }
        [Fact]
        public async Task GetAllChild_IfIsItems_ReturnItems()
        {
            //Arrange
            MakeMockWithIncludeListForRepository();

            // Act
            // Run method which should be tested
            var result = await _controller.GetAllChildAsync();

            // Assert
            Assert.Same(_expectedListCategory, result);
        }
        [Fact]
        public async Task AddChild_IfNewItem_AddItem()
        {
            // Arrange
            MakeMockWithIncludeEntityForRepository();
            var expected = "expected";

            // Act
            // Run method which should be tested
            var newEntity = await _controller.AddChildAsync(2, expected);

            //Assert
            _repositoryMock.Verify(o => o.AddAsync(It.IsAny<Category>()), Times.Exactly(1));
            Assert.Same(expected, newEntity.Name);
            Assert.Equal(_expectedCategory.Parent.Id, _controller.CurrentCategory.ParentId);
        }
        [Fact]
        public async Task AddCategory_IfNewItem_AddItem()
        {
            //Arrange
            var expected = "expected";
            
            // Act
            // Run method which should be tested
            var result = await _controller.AddCategoryAsync(expected);

            //Assert
            Assert.Equal(expected, result.Name);
            _repositoryMock.Verify(o => o.AddAsync(It.IsAny<Category>()), Times.Exactly(1));
        }
        [Fact]
        public async Task SetCurrentCategory_IfItemExists_SetItem()
        {
            // Arrange
            MakeMockWithIncludeEntityForRepository();

            // Act
            // Run method which should be tested
            await _controller.SetCurrentCategoryAsync(1);

            //Assert
            Assert.Same(_expectedCategory, _controller.CurrentCategory);
        }
        [Fact]
        public async Task WalkCategories_IfItemExists_ReturnTrue()
        {
            // Arrange
            MakeMockWithIncludeEntityForRepository();

            // Act
            // Run method which should be tested
            var resultBool = await _controller.WalkCategoriesAsync("1");

            //Assert
            Assert.True(resultBool);
            Assert.Equal(_expectedCategory, _controller.CurrentCategory);
        }
        private void MakeMockWithIncludeListForRepository()
        {
            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            _repositoryMock.Setup(o => o.GetWithIncludeListAsync<Category>(It.IsAny<Func<Category, bool>>(), It.IsAny<Expression<Func<Category, object>>>()))
                .ReturnsAsync(_expectedListCategory);
        }
        private void MakeMockWithIncludeEntityForRepository()
        {
            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test entity
            _repositoryMock.Setup(o => o.GetWithIncludeEntityAsync<Category>(It.IsAny<Func<Category, bool>>()))
                .ReturnsAsync(_expectedCategory);
        }
    }
}
