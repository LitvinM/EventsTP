using AutoMapper;
using Core.Models;
using DataAccess.Entities;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class AddEventUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public AddEventUseCase(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Event eventToAdd)
    {
        var eventEntity = _mapper.Map<EventEntity>(eventToAdd);
        await _eventRepository.AddAsync(eventEntity);
    }
}