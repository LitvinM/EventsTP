using AutoMapper;
using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class SearchEventsUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public SearchEventsUseCase(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<List<Event>> ExecuteAsync(string search)
    {
        var eventEntities = await _eventRepository.GetEventsWithParticipantsAsync();
        return eventEntities
            .Where(e => e.Name.Contains(search) || e.Description.Contains(search))
            .Select(e => _mapper.Map<Event>(e))
            .ToList();
    }
}