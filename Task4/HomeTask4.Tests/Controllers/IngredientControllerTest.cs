using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace HomeTask4.Tests.Controllers
{
    public class IngredientControllerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock; // Create mock object for IUnitOfWork
        private readonly Mock<IRepository> _repositoryMock;  // Create mock object for IRepository
        private readonly IngredientController _controller; // Create controller which should be tested
        private readonly Ingredient _expectedIngredient;
        private readonly List<Ingredient> _expectedListIngredient;
        public IngredientControllerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IRepository>();

            _expectedIngredient = new Ingredient 
            {
                Id = 1, 
                Name = "expected" 
            };

            _expectedListIngredient = new List<Ingredient>()
            { 
                _expectedIngredient
            };
            
            // Simulate "Repository" property to return prevously created mock object for IRepository
            _unitOfWorkMock.SetupGet(o => o.Repository)
                .Returns(_repositoryMock.Object);

            _unitOfWorkMock.Setup(o => o.SaveAsync());

            _controller = new IngredientController(_unitOfWorkMock.Object);
        }
        [Fact]
        public async Task GetIngredients_IfIsItems_ReturnItems()
        {
            //Arrange
            MakeMockGetWithIncludeListForRepository();
            // Act
            // Run method which should be tested
            var result = await _controller.GetIngredientsAsync();

            // Assert
            _repositoryMock.VerifyAll();
            Assert.Same(_expectedListIngredient, result);
        }
        [Fact]
        public async Task AddedIfNew_IfExist_ReturnItem()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository();
            // Act
            // Run method which should be tested
            var entityId = await _controller.AddedIfNewAsync("expected");

            // Assert
            _repositoryMock.VerifyAll();
            Assert.Equal(_expectedIngredient.Id, entityId);
        }
        [Fact]
        public async Task AddedIfNew_IfNew_ReturnNewItem()
        {
            //Arrange
            MakeMockAddForRepository();

            // Act
            // Run method which should be tested
            var entityId = await _controller.AddedIfNewAsync("expected");

            // Assert
            _repositoryMock.VerifyAll();
            Assert.Equal(0, entityId);
        }
        [Fact]
        public async Task FindAndGetIngredient_IfExists_ReturnItem()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository();

            // Act
            // Run method which should be tested
            var result = await _controller.FindAndGetIngredientAsync("expected");

            // Assert
            _repositoryMock.VerifyAll();
            Assert.Same(_expectedIngredient, result);
        }
        [Fact]
        public async Task FindAndGetIngredient_IfNotExists_ReturnNull()
        {
            // Act
            // Run method which should be tested
            var result = await _controller.FindAndGetIngredientAsync("expected");

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetIngredientById_IfItemExists_ReturnItem()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository();

            // Act
            // Run method which should be tested
            var result = await _controller.GetIngredientByIdAsync(1);

            // Assert
            _repositoryMock.VerifyAll();
            Assert.Same(_expectedIngredient, result);
        }
        [Fact]
        public async Task GetIngredientById_IfItemNotExists_ReturnNull()
        {
            // Act
            // Run method which should be tested
            var result = await _controller.GetIngredientByIdAsync(1);

            // Assert
            Assert.Null(result);
        }
        private void MakeMockAddForRepository()
        {
            // Simulate "AddAsync" method from "IRepository" to return test entity
            _repositoryMock.Setup(o => o.AddAsync<Ingredient>(It.IsAny<Ingredient>()))
                .ReturnsAsync((Ingredient x) => x);
        }
        private void MakeMockGetWithIncludeListForRepository()
        {
            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            _repositoryMock.Setup(o => o.GetWithIncludeListAsync<Ingredient>(It.IsAny<Expression<Func<Ingredient, object>>>()))
                .ReturnsAsync(_expectedListIngredient);
        }
        private void MakeMockGetWithIncludeEntityForRepository()
        {
            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test list of entities
            _repositoryMock.Setup(o =>
            o.GetWithIncludeEntityAsync<Ingredient>(It.IsAny<Func<Ingredient, bool>>(), It.IsAny<Expression<Func<Ingredient, object>>>()))
                .ReturnsAsync(_expectedIngredient);
        }
    }
}
