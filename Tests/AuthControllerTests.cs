using System.Security.Claims;
<<<<<<< HEAD
using Application.Abstraction;
using Application.Contracts;
=======
using Application.AdditionalLogic;
using Application.Services;
using Application.Tokens;
using Core.Contracts;
using Core.Models;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using EventsTP.Controllers;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using DataAccess.Entities;
using DataAccess.RepoUOW;
using Microsoft.AspNetCore.Http.HttpResults;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Moq;

namespace Tests;

[TestFixture]
public class AuthControllerTests
{
    private Mock<IAuthService> _authServiceMock;
    private AuthController _authController;

    [SetUp]
    public void SetUp()
    {
<<<<<<< HEAD
        _authServiceMock = new Mock<IAuthService>();
        _authController = new AuthController(_authServiceMock.Object);
=======
        _mockTokenService = new Mock<ITokenService>();

        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _mockHttpContext = new Mock<HttpContext>();
        _mockHttpResponse = new Mock<HttpResponse>();
        _mockResponseCookies = new Mock<IResponseCookies>();

        _mockHttpResponse.SetupGet(r => r.Cookies).Returns(_mockResponseCookies.Object);
        _mockHttpContext.Setup(c => c.Response).Returns(_mockHttpResponse.Object);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(_mockHttpContext.Object);

        _controller = new AuthController(new AuthService(
            _mockTokenService.Object,
            _mockUnitOfWork.Object,
            mockHttpContextAccessor.Object));
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    [Test]
    public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        var loginRequest = new LoginParticipantRequest("", "");

        var expectedResponse = new AuthResponse("");

        _authServiceMock.Setup(s => s.Login(loginRequest)).ReturnsAsync(expectedResponse);

        // Act
        var result = await _authController.Login(loginRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(expectedResponse, okResult.Value);
        _authServiceMock.Verify(s => s.Login(loginRequest), Times.Once);
    }

    [Test]
    public async Task Login_ShouldReturnBadRequest_WhenLoginFails()
    {
<<<<<<< HEAD
        // Arrange
        var loginRequest = new LoginParticipantRequest("", "");
=======
        var loginRequest = new LoginParticipant("user@example.com", "password");
        var expectedToken = "user-token";
        var participant = Participant.Create("name", "surname", DateOnly.MinValue, "user@example.com", PasswordHasher.Generate("password"));
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

        _authServiceMock.Setup(s => s.Login(loginRequest)).ReturnsAsync((AuthResponse)null);

        // Act
        var result = await _authController.Login(loginRequest);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
        _authServiceMock.Verify(s => s.Login(loginRequest), Times.Once);
    }
}
