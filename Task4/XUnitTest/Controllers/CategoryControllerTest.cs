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
    public class CategoryControllerTest
    {
        [Fact]
        public async void GetCategories()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            
            var name = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();

            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            repositoryMock.Setup(o => o.GetWithIncludeListAsync<Category>(It.IsAny<Func<Category,bool>>(), It.IsAny<Expression<Func<Category, object>>>()))
                .ReturnsAsync(new List<Category>
                {
                    new Category { Id = 1, Name=name,Parent=null, ParentId=null, Children = null},
                    new Category { Id = 2, Name=name2,Parent=null,ParentId=null, Children = null}
                });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new CategoryController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var entities = await controller.GetCategoriesAsync();
            var firstEntity = entities.FirstOrDefault();
            var secondEntity = entities.FirstOrDefault(i => i.Id == 2);

            // Assert
            Assert.Equal(name, firstEntity.Name);
            Assert.Equal(name2, secondEntity.Name);
            Assert.Equal(2, entities.Count());
        }
        [Fact]
        public async void GetAllChild()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var name = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();

            var parent = new Category { 
                Id = rnd.Next(), 
                Name = Guid.NewGuid().ToString() 
            };
            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            repositoryMock.Setup(o => o.GetWithIncludeListAsync<Category>(It.IsAny<Func<Category, bool>>(), It.IsAny<Expression<Func<Category, object>>>()))
                .ReturnsAsync(new List<Category>
                {
                    new Category { Id = 1, Name=name,Parent=parent, ParentId=parent.Id, Children = null},
                    new Category { Id = 2, Name=name2,Parent=parent,ParentId=parent.Id, Children = null}
                });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new CategoryController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested

            var entities = await controller.GetAllChildAsync();
            var firstEntity = entities.FirstOrDefault();
            var secondEntity = entities.FirstOrDefault(i=>i.Id==2);

            // Assert
            Assert.Equal(name, firstEntity.Name);
            Assert.Equal(parent.Id, firstEntity.ParentId);
            Assert.Equal(parent, firstEntity.Parent);

            Assert.Equal(name2, secondEntity.Name);
            Assert.Equal(parent.Id, secondEntity.ParentId);
            Assert.Equal(parent, secondEntity.Parent);

            Assert.Equal(2, entities.Count());
        }
        [Fact]
        public async void AddChild_ShouldWork()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var name = Guid.NewGuid().ToString();
            var parentId = rnd.Next();
            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            repositoryMock.Setup(o => o.GetWithIncludeListAsync<Category>(It.IsAny<Func<Category, bool>>(), It.IsAny<Expression<Func<Category, object>>>()))
                .ReturnsAsync(new List<Category>
                {
                    new Category { Id = 1, Name=Guid.NewGuid().ToString()}
                });

            // Simulate "AddAsync" method from "IRepository" to return new test entity
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync((Category x) => x);

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());
            // Create controller which should be tested
            var controller = new CategoryController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var newEntity = await controller.AddChildAsync(parentId, name);

            //Assert
            repositoryMock.Verify(o => o.AddAsync(It.IsAny<Category>()), Times.Exactly(1));
            Assert.Equal(name, newEntity.Name);
            Assert.Equal(parentId, newEntity.ParentId);
            Assert.Equal(newEntity, controller.CurrentCategory);
        }
        [Fact]
        public async void AddCategory_ShouldWork()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository

            var name = Guid.NewGuid().ToString();
            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            repositoryMock.Setup(o => o.GetWithIncludeListAsync<Category>(It.IsAny<Func<Category, bool>>(), It.IsAny<Expression<Func<Category, object>>>()))
                .ReturnsAsync(new List<Category>
                {
                    new Category { Id = 1, Name=Guid.NewGuid().ToString()}
                });

            // Simulate "AddAsync" method from "IRepository" to return new test entity
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync((Category x) => x);

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());
            // Create controller which should be tested
            var controller = new CategoryController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested

            var newEntity = await controller.AddCategoryAsync(name);

            //Assert
            repositoryMock.Verify(o => o.AddAsync(It.IsAny<Category>()), Times.Exactly(1));
            Assert.Equal(name, newEntity.Name);
        }
        [Fact]
        public async void SetCurrentCategory_ShouldWork()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var name = Guid.NewGuid().ToString();
            var id = rnd.Next();
            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test entity
            repositoryMock.Setup(o => o.GetWithIncludeEntityAsync<Category>(It.IsAny<Func<Category, bool>>()))
                .ReturnsAsync(
                    new Category { 
                        Id = id, 
                        Name=name
                    }
                );

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());
            // Create controller which should be tested
            var controller = new CategoryController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested

            await controller.SetCurrentCategoryAsync(id);

            //Assert
            Assert.Equal(id, controller.CurrentCategory.Id);
            Assert.Equal(name, controller.CurrentCategory.Name);
        }
        [Fact]
        public async void WalkCategories_ShouldWork()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var name = Guid.NewGuid().ToString();
            var id = rnd.Next();
            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test entity
            repositoryMock.Setup(o => o.GetWithIncludeEntityAsync<Category>(It.IsAny<Func<Category, bool>>()))
                .ReturnsAsync(
                    new Category
                    {
                        Id = id,
                        Name = name
                    }
                );

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new CategoryController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested
            var resultBool = await controller.WalkCategoriesAsync(id.ToString());

            //Assert
            Assert.True(resultBool);
            Assert.Equal(id, controller.CurrentCategory.Id);
            Assert.Equal(name, controller.CurrentCategory.Name);
        }
    }
}
