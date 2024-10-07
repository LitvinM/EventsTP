<<<<<<< HEAD
using Core.Models;
=======
using Application.Services;
using Core.Abstraction;
using Core.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using EventsTP.Controllers;
using DataAccess.RepoUOW;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

namespace Tests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstraction;
using EventsTP.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class EventControllerTests
{
    private Mock<IEventService> _eventServiceMock;
    private EventController _eventController;

    [SetUp]
    public void SetUp()
    {
<<<<<<< HEAD
        _eventServiceMock = new Mock<IEventService>();
        _eventController = new EventController(_eventServiceMock.Object);
=======
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockEventService = new Mock<IEventService>();
        _controller = new EventController(new EventService(_mockUnitOfWork.Object));
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    [Test]
    public async Task GetAllEvents_ShouldReturnOk_WhenEventsExist()
    {
        // Arrange
        var expectedEvents = new List<Event>
        {
<<<<<<< HEAD
            Event.Create("", "", DateTime.Now, "", "", 10, "")
=======
            Event.Create("name1", "desc", DateTime.Now, "place", "cat", 1000,"image"),
            Event.Create("name2", "desc", DateTime.Now, "place", "cat", 1000,"image")
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        };
        _eventServiceMock.Setup(s => s.GetAllEvents()).ReturnsAsync(expectedEvents);

        // Act
        var result = await _eventController.GetAllEvents();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(expectedEvents, okResult.Value);
        _eventServiceMock.Verify(s => s.GetAllEvents(), Times.Once);
    }

    [Test]
    public async Task GetAllEventsWithParticipants_ShouldReturnOk_WhenEventsExist()
    {
        // Arrange
        var expectedEvents = new List<Event>
        {
<<<<<<< HEAD
            Event.Create("", "", DateTime.Now, "", "", 10, "")
=======
            Event.Create("name1", "desc", DateTime.Now, "place", "cat", 1000,"image"),
            Event.Create("name2", "desc", DateTime.Now, "place", "cat", 1000,"image")
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        };
        _eventServiceMock.Setup(s => s.GetEventsWithParticipantsAsync()).ReturnsAsync(expectedEvents);

        // Act
        var result = await _eventController.GetAllEventsWithParticipants();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(expectedEvents, okResult.Value);
        _eventServiceMock.Verify(s => s.GetEventsWithParticipantsAsync(), Times.Once);
    }

    [Test]
    public async Task Search_ShouldReturnOk_WhenEventsFound()
    {
        // Arrange
        var searchQuery = "sample search";
        var expectedEvents = new List<Event>
        {
<<<<<<< HEAD
            Event.Create("", "", DateTime.Now, "", "", 10, "")
=======
            Event.Create("Special EventEntity", "Special", DateTime.Now, "place", "cat", 1000,"image"),
            Event.Create("name2", "desc", DateTime.Now, "place", "cat", 1000,"image")
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        };
        _eventServiceMock.Setup(s => s.SearchEvents(searchQuery)).ReturnsAsync(expectedEvents);

        // Act
        var result = await _eventController.Search(searchQuery);

<<<<<<< HEAD
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(expectedEvents, okResult.Value);
        _eventServiceMock.Verify(s => s.SearchEvents(searchQuery), Times.Once);
=======
        Assert.IsNotNull(result, "Result should not be null.");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 OK.");
        var foundEvents = result.Value as List<Event>;
        Assert.AreEqual(1, foundEvents.Count, "There should be 1 event matching the search keyword.");
        Assert.AreEqual("Special EventEntity", foundEvents.First().Name, "The event name should match the search keyword.");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    [Test]
    public async Task GetEventById_ShouldReturnOk_WhenEventExists()
    {
<<<<<<< HEAD
        // Arrange
        var id = Guid.NewGuid();
        var expectedEvent = Event.Create("", "", DateTime.Now, "", "", 10, "");
        _eventServiceMock.Setup(s => s.GetEventById(id)).ReturnsAsync(expectedEvent);

        // Act
        var result = await _eventController.GetEventById(id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(expectedEvent, okResult.Value);
        _eventServiceMock.Verify(s => s.GetEventById(id), Times.Once);
    }

    [Test]
    public async Task AddParticipantToEvent_ShouldReturnNoContent_WhenParticipantIsAdded()
    {
        // Arrange
        var participantEmail = "participant@example.com";
        var eventId = Guid.NewGuid();
=======
        var eventEntity = Event.Create("name1", "desc", DateTime.Now, "place", "cat", 1000, "image");
        
        var participant = Participant.Create("name", "surname", DateOnly.MinValue, "participant@example.com", "1234");

        participant.Events = new List<Event> { eventEntity };
    
        _mockUnitOfWork.Setup(u => u.Events.GetByIdAsync(eventEntity.Id)).ReturnsAsync(eventEntity);
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

        // Act
        var result = await _eventController.AddParticipantToEvent(participantEmail, eventId);

<<<<<<< HEAD
        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
        _eventServiceMock.Verify(s => s.AddParticipant(eventId, participantEmail), Times.Once);
    }

    [Test]
    public async Task AddParticipantToEvent_ShouldReturnNoContent_WhenEmailIsEmpty()
    {
        // Arrange
        var participantEmail = string.Empty;
        var eventId = Guid.NewGuid();

        // Act
        var result = await _eventController.AddParticipantToEvent(participantEmail, eventId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
        _eventServiceMock.Verify(s => s.AddParticipant(eventId, participantEmail), Times.Never);
    }
=======
        var result = await _controller.GetEventById(eventEntity.Id) as OkObjectResult;

        Assert.IsNotNull(result, "Result should not be null.");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 OK.");
        var returnedEvent = result.Value as Event;
        Assert.IsNotNull(returnedEvent, "EventEntity should not be null.");
        Assert.AreEqual(eventEntity.Id, returnedEvent.Id, "EventEntity ID should match.");
    }
    
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
}
