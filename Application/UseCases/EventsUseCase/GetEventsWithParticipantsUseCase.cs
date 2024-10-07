using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class GetEventsWithParticipantsUseCase
{
    private readonly IEventRepository _eventRepository;

    public GetEventsWithParticipantsUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<Event>> ExecuteAsync()
    {
        var eventsEntities = await _eventRepository.GetEventsWithParticipantsAsync();
        return eventsEntities.Select(e => 
            Event.Create(e.Name, e.Description, e.TimeAndDate, e.Place, e.Category, e.ParticipantsMaxAmount, e.Image));
    }
}
