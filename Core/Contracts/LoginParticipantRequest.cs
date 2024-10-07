namespace Core.Contracts;

public record LoginParticipantRequest(
    string Email,
    string Password
    );