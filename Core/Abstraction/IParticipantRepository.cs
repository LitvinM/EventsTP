using Core.Models;

namespace Core.Abstraction;


public interface IParticipantRepository : IRepository<Participant>
{
    Task<IEnumerable<Participant>> GetParticipantsWithEventsAsync();
    Task<Participant> GetByEmailAsync(string email);
}
