using Application.Contracts;
using Core.Models;

namespace Application.Abstraction;

public interface IParticipantService
{
    Task<IEnumerable<Participant>> GetAllParticipants();
    Task AddParticipant(CreateParticipantRequest request);
    Task UpdateParticipant(Guid id, Participant participant);
    Task DeleteParticipant(Guid id);
    Task<Participant> GetParticipantById(Guid id);
    Task<Participant> GetParticipantByEmail(string email);
}