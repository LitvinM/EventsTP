using System.Security.Claims;
<<<<<<< HEAD
using Application.Abstraction;
using Application.AdditionalLogic;
using Application.Tokens;
using DataAccess.RepoUOW;
using Microsoft.AspNetCore.Http;
using Application.Contracts;
=======
using Application.Tokens;
using Core.Abstraction;
using DataAccess.RepoUOW;
using Microsoft.AspNetCore.Http;
using Application.AdditionalLogic;
using Core.Contracts;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _context;

    public AuthService(ITokenService tokenService, IUnitOfWork unitOfWork, IHttpContextAccessor context)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _context = context;
    }
    
<<<<<<< HEAD
    public async Task<AuthResponse> Login(LoginParticipantRequest loginRequest)
=======
    public async Task<AuthResponse> Login(LoginParticipant loginRequest)
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    {
        var participants = (await _unitOfWork.Participants.GetAllAsync()).ToList();
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax
        };
        if (loginRequest is { Email: "admin@example.com", Password: "password" })
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Email, loginRequest.Email),
                new (ClaimTypes.Role, "Admin")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);

            _context.HttpContext!.Response.Cookies.Append("cook", accessToken, cookieOptions);

            return new AuthResponse(accessToken);
        }

        var participant = participants.FirstOrDefault(p => p.Email.Equals(loginRequest.Email));
        
        if (participant != null && PasswordHasher.Verify(loginRequest.Password, participant.Password))
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Email, loginRequest.Email),
                new (ClaimTypes.Role, "User")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            
            _context.HttpContext!.Response.Cookies.Append("cook", accessToken, cookieOptions);

            return new AuthResponse(accessToken);
        }

        throw new UnauthorizedAccessException("Access is denied.");
    }
}   