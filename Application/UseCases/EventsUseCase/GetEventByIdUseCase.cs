using AutoMapper;
using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class GetEventByIdUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventByIdUseCase(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Event> ExecuteAsync(Guid eventId)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException("Event not found");
        }

        return _mapper.Map<Event>(eventEntity);
    }
}

