<<<<<<< HEAD
using System;
using System.Threading.Tasks;
using Application.Abstraction;
using Application.Contracts;
using Core.Models;
using EventsTP.Controllers;
using Microsoft.AspNetCore.Mvc;
=======
using Application.Services;
using AutoMapper;
using Core.Contracts;
using Core.Models;
using EventsTP.Controllers;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using DataAccess.RepoUOW;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AdminControllerTests
    {
<<<<<<< HEAD
        private Mock<IAdminService> _adminServiceMock;
        private AdminController _adminController;

        [SetUp]
        public void SetUp()
        {
            _adminServiceMock = new Mock<IAdminService>();
            _adminController = new AdminController(_adminServiceMock.Object);
        }

        [Test]
        public async Task AddEvent_ShouldReturnOk_WhenEventIsAdded()
        {
            // Arrange
            var request = new CreateEventRequest("", "", DateTime.Now, "", "", "", 10);
            
            // Act
            var result = await _adminController.AddEvent(request);
            
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual("Added", okResult.Value);
            _adminServiceMock.Verify(s => s.AddEvent(request), Times.Once);
        }

        [Test]
        public async Task UpdateEvent_ShouldReturnNoContent_WhenEventIsUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = Event.Create("", "", DateTime.Now, "", "", 10, "");
            
            // Act
            var result = await _adminController.UpdateEvent(entity, id);
            
            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _adminServiceMock.Verify(s => s.UpdateEvent(entity, id), Times.Once);
        }

        [Test]
        public async Task DeleteEvent_ShouldReturnNoContent_WhenEventIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            
            // Act
            var result = await _adminController.DeleteEvent(id);
            
            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _adminServiceMock.Verify(s => s.DeleteEvent(id), Times.Once);
        }
    }
}
=======
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _controller = new AdminController(new AdminService(_mockUnitOfWork.Object, _mockMapper.Object));
    }

    [Test]
    public async Task AddEvent_ShouldReturnOk_WhenEventIsAdded()
    {
        var request = new CreateEventRequest ("name", "desc", DateTime.Now, "place", "cat", "image", 1000);
        var eventEntity = Event.Create("name", "desc", DateTime.Now, "place", "cat", 1000,"image");
        _mockMapper.Setup(m => m.Map<Event>(request)).Returns(eventEntity);
        _mockUnitOfWork.Setup(u => u.Events.AddAsync(eventEntity)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.SaveDbAsync()).ReturnsAsync(1);

        // Act
        var result = await _controller.AddEvent(request) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual($"Added", result.Value);
        _mockUnitOfWork.Verify(u => u.Events.AddAsync(eventEntity), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveDbAsync(), Times.Once);
    }

    [Test]
    public async Task UpdateEvent_ShouldReturnNoContent_WhenEventIsUpdated()
    {
        // Arrange
        var eventEntity = Event.Create("", "", DateTime.Now, "", "", 0, "");
        _mockUnitOfWork.Setup(u => u.Events.Update(eventEntity)).Verifiable();

        // Act
        var result = await _controller.UpdateEvent(eventEntity, eventEntity.Id) as NoContentResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode);
        _mockUnitOfWork.Verify(u => u.Events.Update(eventEntity), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveDbAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteEvent_ShouldReturnNoContent_WhenEventIsDeleted()
    {
        // Arrange
        var eventEntity = Event.Create("", "", DateTime.Now, "", "", 0, "");
        _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventEntity.Id)).ReturnsAsync(eventEntity);
        _mockUnitOfWork.Setup(u => u.Events.Delete(eventEntity)).Verifiable();

        // Act
        var result = await _controller.DeleteEvent(eventEntity.Id) as NoContentResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(204, result!.StatusCode);
        _mockUnitOfWork.Verify(u => u.Events.GetByIdAsync(eventEntity.Id), Times.Once);
        _mockUnitOfWork.Verify(u => u.Events.Delete(eventEntity), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveDbAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteEvent_ShouldThrowKeyNotFoundException_WhenEventIsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(id)).ReturnsAsync((Event)null);

        // Act & Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await _controller.DeleteEvent(id));
        _mockUnitOfWork.Verify(u => u.Events.GetByIdAsync(id), Times.Once);
    }
}
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
