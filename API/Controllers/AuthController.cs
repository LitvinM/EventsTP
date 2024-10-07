<<<<<<< HEAD
using Application.Abstraction;
using Application.Contracts;
=======
using Core.Abstraction;
using Core.Contracts;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Microsoft.AspNetCore.Mvc;

namespace EventsTP.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
<<<<<<< HEAD
    public async Task<IActionResult> Login([FromBody] LoginParticipantRequest loginRequest)
=======
    public async Task<IActionResult> Login([FromBody] LoginParticipant loginRequest)
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    {
        var authResponse = await _authService.Login(loginRequest);
        return Ok(authResponse);
    }

}
