<<<<<<< HEAD
using Application.Abstraction;
using Application.Contracts;
using Application.Services;
using AutoMapper;
using Core.Models;
=======
using Application.Services;
using AutoMapper;
using Core.Contracts;
using Core.Models;
using DataAccess.Entities;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using EventsTP.Controllers;
using Microsoft.AspNetCore.Mvc;
using DataAccess.RepoUOW;
using Moq;

namespace Tests
{
   [TestFixture]
    public class ParticipantControllerTests
    {
        private Mock<IParticipantService> _participantServiceMock;
        private ParticipantController _participantController;

        [SetUp]
        public void SetUp()
        {
<<<<<<< HEAD
            _participantServiceMock = new Mock<IParticipantService>();
            _participantController = new ParticipantController(_participantServiceMock.Object);
=======
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();

            _controller = new ParticipantController(new ParticipantService(_mockUnitOfWork.Object, _mockMapper.Object));
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        }

        [Test]
        public async Task GetAllParticipants_ShouldReturnOk_WhenParticipantsExist()
        {
<<<<<<< HEAD
            // Arrange
            var expectedParticipants = new List<Participant> { Participant.Create("", "", DateOnly.MaxValue, "participant@example.com", "") };
            _participantServiceMock.Setup(s => s.GetAllParticipants()).ReturnsAsync(expectedParticipants);
=======
            var participants = new List<Participant>
            {
                Participant.Create("name", "surname", DateOnly.MinValue, "test1@example.com", "1234"),
                Participant.Create("name", "surname", DateOnly.MinValue, "test1@example.com", "1234")
            };
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

            // Act
            var result = await _participantController.GetAllParticipants();

<<<<<<< HEAD
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedParticipants, okResult.Value);
            _participantServiceMock.Verify(s => s.GetAllParticipants(), Times.Once);
=======
            var result = await _controller.GetAllParticipants() as OkObjectResult;

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(200, result.StatusCode, "Status code should be 200 OK.");
            var returnedParticipants = result.Value as List<Participant>;
            Assert.IsNotNull(returnedParticipants, "Participants should not be null.");
            Assert.AreEqual(participants.Count, returnedParticipants.Count, "ParticipantEntity count should match.");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        }

        [Test]
        public async Task AddParticipant_ShouldReturnOk_WhenParticipantIsAdded()
        {
            // Arrange
            var request = new CreateParticipantRequest("", "", DateOnly.MaxValue, "");

<<<<<<< HEAD
            // Act
            var result = await _participantController.AddParticipant(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual("Added", okResult.Value);
            _participantServiceMock.Verify(s => s.AddParticipant(request), Times.Once);
=======
            var hashedPassword = "hashedpassword";
            var participant = Participant.Create("name", "surname", DateOnly.MinValue, "test1@example.com", "1234");

            _mockUnitOfWork.Setup(u => u.Participants.GetAllAsync()).ReturnsAsync(new List<Participant>());
            _mockMapper.Setup(m => m.Map<Participant>(It.IsAny<CreateParticipantRequest>())).Returns(participant);

            _mockUnitOfWork.Setup(u => u.Participants.AddAsync(participant)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveDbAsync()).ReturnsAsync(1);

            var result = await _controller.AddParticipant(request) as OkObjectResult;

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(200, result.StatusCode, "Status code should be 200 OK.");
            Assert.AreEqual($"Added", result.Value, "Message should match.");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        }

        [Test]
        public async Task UpdateParticipant_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
<<<<<<< HEAD
            // Arrange
            var id = Guid.NewGuid();
            var participant = Participant.Create("", "", DateOnly.MaxValue, "participant@example.com", "");
=======
            var participant = Participant.Create("name", "surname", DateOnly.MinValue, "test1@example.com", "1234");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

            // Act
            var result = await _participantController.UpdateParticipant(id, participant);

<<<<<<< HEAD
            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _participantServiceMock.Verify(s => s.UpdateParticipant(id, participant), Times.Once);
=======
            var result = await _controller.UpdateParticipant(participant.Id, participant);

            Assert.IsInstanceOf<NoContentResult>(result, "Result should be NoContent.");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        }

        [Test]
        public async Task DeleteParticipant_ShouldReturnNoContent_WhenDeleteIsSuccessful()
        {
<<<<<<< HEAD
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _participantController.DeleteParticipant(id);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            _participantServiceMock.Verify(s => s.DeleteParticipant(id), Times.Once);
=======
            var participant = Participant.Create("name", "surname", DateOnly.MinValue, "test1@example.com", "1234");

            _mockUnitOfWork.Setup(u => u.Participants.GetByIdAsync(participant.Id)).ReturnsAsync(participant);
            _mockUnitOfWork.Setup(u => u.Participants.Delete(participant));
            _mockUnitOfWork.Setup(u => u.SaveDbAsync()).ReturnsAsync(1);

            var result = await _controller.DeleteParticipant(participant.Id);

            Assert.IsInstanceOf<NoContentResult>(result, "Result should be NoContent.");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        }

        [Test]
        public async Task GetParticipantById_ShouldReturnOk_WhenParticipantExists()
        {
<<<<<<< HEAD
            // Arrange
            var id = Guid.NewGuid();
            var expectedParticipant = Participant.Create("", "", DateOnly.MaxValue, "participant@example.com", "");
            _participantServiceMock.Setup(s => s.GetParticipantById(id)).ReturnsAsync(expectedParticipant);

            // Act
            var result = await _participantController.GetParticipantById(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedParticipant, okResult.Value);
            _participantServiceMock.Verify(s => s.GetParticipantById(id), Times.Once);
=======
            var participant = Participant.Create("name", "surname", DateOnly.MinValue, "test1@example.com", "1234");

            _mockUnitOfWork.Setup(u => u.Participants.GetByIdAsync(participant.Id)).ReturnsAsync(participant);

            var result = await _controller.GetParticipantById(participant.Id) as OkObjectResult;

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(200, result.StatusCode, "Status code should be 200 OK.");
            var returnedParticipant = result.Value as Participant;
            Assert.IsNotNull(returnedParticipant, "ParticipantEntity should not be null.");
            Assert.AreEqual(participant.Id, returnedParticipant.Id, "ParticipantEntity ID should match.");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        }

        [Test]
        public async Task GetParticipantByEmail_ShouldReturnOk_WhenParticipantExists()
        {
<<<<<<< HEAD
            // Arrange
            var email = "participant@example.com";
            var expectedParticipant = Participant.Create("", "", DateOnly.MaxValue, "participant@example.com", "");
            _participantServiceMock.Setup(s => s.GetParticipantByEmail(email)).ReturnsAsync(expectedParticipant);
=======
            var email = "test@example.com";
            var participant = Participant.Create("name", "surname", DateOnly.MinValue, email, "1234");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

            // Act
            var result = await _participantController.GetParticipantByEmail(email);

<<<<<<< HEAD
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedParticipant, okResult.Value);
            _participantServiceMock.Verify(s => s.GetParticipantByEmail(email), Times.Once);
=======
            var result = await _controller.GetParticipantByEmail(email) as OkObjectResult;

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(200, result.StatusCode, "Status code should be 200 OK.");
            var returnedParticipant = result.Value as Participant;
            Assert.IsNotNull(returnedParticipant, "ParticipantEntity should not be null.");
            Assert.AreEqual(email, returnedParticipant.Email, "ParticipantEntity email should match.");
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        }
    }
}
