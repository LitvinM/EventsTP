using Application.Contracts;

namespace Application.Abstraction;

public interface IAuthService
{
    Task<AuthResponse> Login(LoginParticipantRequest loginRequest);
}