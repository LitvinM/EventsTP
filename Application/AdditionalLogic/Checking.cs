<<<<<<< HEAD
using Application.Contracts;
=======
using Core.Contracts;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Core.Models;

namespace Application.AdditionalLogic;

public static class Checking
{
    public static bool CheckParticipantData(CreateParticipantRequest request, List<Participant> participants)
    {
        if (request.Name == "" || request.Surname == "" ||
            request.Email == "" || request.Password == "")
            return false;

        return participants.FirstOrDefault(p => p.Email == request.Email) == null;
    }
}