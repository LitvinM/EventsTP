<<<<<<<< HEAD:Application/Contracts/CreateEventRequest.cs
namespace Application.Contracts;
========
namespace Core.Contracts;
>>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79:Core/Contracts/CreateEventRequest.cs

public record CreateEventRequest(
    string Name,
    string Description,
    DateTime TimeAndDate,
    string Place,
    string Category,
    string Image,
    uint ParticipantsMaxAmount
);