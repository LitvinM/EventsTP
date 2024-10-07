using Core.Contracts;

namespace Core.Abstraction;

public interface IAuthService
{
    Task<AuthResponse> Login(LoginParticipant loginRequest);
}