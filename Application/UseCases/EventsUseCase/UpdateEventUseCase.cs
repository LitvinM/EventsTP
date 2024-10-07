using AutoMapper;
using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class UpdateEventUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public UpdateEventUseCase(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Event eventToUpdate)
    {
        var existingEventEntity = await _eventRepository.GetByIdAsync(eventToUpdate.Id);
        
        if (existingEventEntity == null)
        {
            throw new KeyNotFoundException("Event not found");
        }

        _mapper.Map(eventToUpdate, existingEventEntity);
        _eventRepository.Update(existingEventEntity);
    }
}
