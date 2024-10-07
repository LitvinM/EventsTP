using AutoMapper;
using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class GetAllEventsUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetAllEventsUseCase(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Event>> ExecuteAsync()
    {
        var eventEntities = await _eventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<Event>>(eventEntities);
    }
}