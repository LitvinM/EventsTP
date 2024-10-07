using Core.Models;

namespace Core.Abstraction;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAllEvents();
    Task<Event> GetEventById(Guid id);
    Task<IEnumerable<Event>> GetEventsWithParticipantsAsync();
    Task AddEvent(Event entity);
    Task UpdateEvent(Event entity);
    Task DeleteEvent(Event entity);
    
    Task AddParticipant(Guid id, string email);
    Task<List<Event>> SearchEvents(string search);
}