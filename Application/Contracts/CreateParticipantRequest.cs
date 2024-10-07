<<<<<<<< HEAD:Application/Contracts/CreateParticipantRequest.cs
namespace Application.Contracts;
========
namespace Core.Contracts;
>>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79:Core/Contracts/CreateParticipantRequest.cs

public record CreateParticipantRequest(
    string Name,
    string Surname,
    DateOnly DateOfBirth,
    string Email
)
{
    public string Password { get; set; } = "";
}