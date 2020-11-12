using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace XUnitTest.Controllers
{
    public class IngredientControllerTest
    {
        [Fact]
        public async void GetIngredients()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository

            var name = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();

            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            repositoryMock.Setup(o => o.GetWithIncludeListAsync<Ingredient>( It.IsAny<Expression<Func<Ingredient, object>>>()))
                .ReturnsAsync(new List<Ingredient>
                {
                    new Ingredient { Id = 1, Name=name},
                    new Ingredient { Id = 2, Name=name2}
                });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new IngredientController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var entities = await controller.GetIngredientsAsync();
            var firstEntity = entities.FirstOrDefault();
            var secondEntity = entities.FirstOrDefault(i => i.Id == 2);
            
            // Assert
            Assert.Equal(name, firstEntity.Name);
            Assert.Equal(name2, secondEntity.Name);
            Assert.Equal(2, entities.Count());
        }
        [Fact]
        public async void AddedIfNew_If_Exist()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository

            var name = Guid.NewGuid().ToString();

            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test entity
            repositoryMock.Setup(o => o.GetWithIncludeEntityAsync<Ingredient>(It.IsAny<Func<Ingredient, bool>>(),It.IsAny<Expression<Func<Ingredient, object>>>()))
                .ReturnsAsync(new Ingredient 
                    { 
                        Id = 1, Name = name
                    });

            // Simulate "AddAsync" method from "IRepository" to return test entity
            repositoryMock.Setup(o => o.AddAsync<Ingredient>(It.IsAny<Ingredient>()))
                .ReturnsAsync((Ingredient x)=> x);

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new IngredientController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var entityId = await controller.AddedIfNewAsync(name);

            // Assert
            Assert.Equal(1, entityId);
        }
        [Fact]
        public async void AddedIfNew_If_New()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository

            var name = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();

            // Simulate "AddAsync" method from "IRepository"
            repositoryMock.Setup(o => o.AddAsync<Ingredient>(It.IsAny<Ingredient>()))
                .ReturnsAsync((Ingredient x) => x);

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new IngredientController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var entityId = await controller.AddedIfNewAsync(name2);

            // Assert
            Assert.Equal(0, entityId);
            repositoryMock.Verify(o => o.AddAsync(It.IsAny<Ingredient>()), Times.Exactly(1));
        }
        [Fact]
        public async void FindAndGetIngredient()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();
            
            var id = rnd.Next();
            var name = Guid.NewGuid().ToString();

            // Simulate "GetWithIncludeEntityAsync" method from "IRepository"
            repositoryMock.Setup(o => o.GetWithIncludeEntityAsync<Ingredient>(It.IsAny<Func<Ingredient, bool>>(), It.IsAny<Expression<Func<Ingredient, object>>>()))
                .ReturnsAsync( new Ingredient
                {
                    Id = id,
                    Name = name
                });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new IngredientController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var entity = await controller.FindAndGetIngredientAsync(name);

            // Assert
            Assert.Equal(id, entity.Id);
            Assert.Equal(name, entity.Name);
        }
        [Fact]
        public async void GetIngredientByAsync()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var id = rnd.Next();
            var name = Guid.NewGuid().ToString();

            // Simulate "GetWithIncludeEntityAsync" method from "IRepository"
            repositoryMock.Setup(o => o.GetWithIncludeEntityAsync<Ingredient>(It.IsAny<Func<Ingredient, bool>>(), It.IsAny<Expression<Func<Ingredient, object>>>()))
                .ReturnsAsync(new Ingredient
                {
                    Id = id,
                    Name = name
                });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new IngredientController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var entity = await controller.GetIngredientByIdAsync(id);

            // Assert
            Assert.Equal(id, entity.Id);
            Assert.Equal(name, entity.Name);
        }
    }
}
