using Core.Models;

namespace Core.Abstraction;

public interface IEventRepository : IRepository<Event>
{
    Task<IEnumerable<Event>> GetEventsWithParticipantsAsync();
}