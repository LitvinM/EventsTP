using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class DeleteEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task ExecuteAsync(Event e)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(e.Id);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException("Event not found");
        }

        _eventRepository.Delete(eventEntity);
    }
}

