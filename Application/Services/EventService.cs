<<<<<<< HEAD
using Application.Abstraction;
using Application.UseCases.EventsUseCase;
=======
using Core.Abstraction;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Core.Models;
using DataAccess.RepoUOW;

namespace Application.Services;
<<<<<<< HEAD
public class EventService : IEventService
{
    private readonly GetAllEventsUseCase _getAllEventsUseCase;
    private readonly GetEventByIdUseCase _getEventByIdUseCase;
    private readonly GetEventsWithParticipantsUseCase _getEventsWithParticipantsUseCase;
    private readonly AddEventUseCase _addEventUseCase;
    private readonly UpdateEventUseCase _updateEventUseCase;
    private readonly DeleteEventUseCase _deleteEventUseCase;
    private readonly AddParticipantToEventUseCase _addParticipantToEventUseCase;
    private readonly SearchEventsUseCase _searchEventsUseCase;

    public EventService(
        GetAllEventsUseCase getAllEventsUseCase,
        GetEventByIdUseCase getEventByIdUseCase,
        GetEventsWithParticipantsUseCase getEventsWithParticipantsUseCase,
        AddEventUseCase addEventUseCase,
        UpdateEventUseCase updateEventUseCase,
        DeleteEventUseCase deleteEventUseCase,
        AddParticipantToEventUseCase addParticipantToEventUseCase,
        SearchEventsUseCase searchEventsUseCase)
    {
        _getAllEventsUseCase = getAllEventsUseCase;
        _getEventByIdUseCase = getEventByIdUseCase;
        _getEventsWithParticipantsUseCase = getEventsWithParticipantsUseCase;
        _addEventUseCase = addEventUseCase;
        _updateEventUseCase = updateEventUseCase;
        _deleteEventUseCase = deleteEventUseCase;
        _addParticipantToEventUseCase = addParticipantToEventUseCase;
        _searchEventsUseCase = searchEventsUseCase;
=======

public class EventService : IEventService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public EventService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task<IEnumerable<Event>> GetAllEvents()
    {
<<<<<<< HEAD
        return await _getAllEventsUseCase.ExecuteAsync();
=======
        return await _unitOfWork.Events.GetAllAsync();
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task<Event> GetEventById(Guid id)
    {
<<<<<<< HEAD
        return await _getEventByIdUseCase.ExecuteAsync(id);
    }

    public async Task<IEnumerable<Event>> GetEventsWithParticipantsAsync()
    {
        return await _getEventsWithParticipantsUseCase.ExecuteAsync();
=======
        var e = await _unitOfWork.Events.GetByIdAsync(id);
        if (e == null)
        {
            throw new KeyNotFoundException("Not found");
        }
        foreach (var participant in await _unitOfWork.Participants.GetAllAsync())
        {
            if (participant.Events.Contains(e))
            {
                e.Participants.ToList().Add(participant);
            }
        }
        return e;
    }
    
    public async Task<IEnumerable<Event>> GetEventsWithParticipantsAsync()
    {
        return await _unitOfWork.Events.GetEventsWithParticipantsAsync();
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task AddEvent(Event entity)
    {
<<<<<<< HEAD
        await _addEventUseCase.ExecuteAsync(entity);
=======
        await _unitOfWork.Events.AddAsync(entity);
        
        await _unitOfWork.SaveDbAsync();
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task UpdateEvent(Event entity)
    {
<<<<<<< HEAD
        await _updateEventUseCase.ExecuteAsync(entity);
    }

    public async Task DeleteEvent(Event entity)
    {
        await _deleteEventUseCase.ExecuteAsync(entity);
    }

    public async Task AddParticipant(Guid eventId, string email)
    {
        await _addParticipantToEventUseCase.ExecuteAsync(eventId, email);
=======
        _unitOfWork.Events.Update(entity);
        
        await _unitOfWork.SaveDbAsync();
    }
    public async Task DeleteEvent(Event entity)
    {
        _unitOfWork.Events.Delete(entity);
        await _unitOfWork.SaveDbAsync();
    }
    
    public async Task AddParticipant(Guid id, string email)
    {
        var participant = _unitOfWork.Participants.GetAllAsync().Result.FirstOrDefault(p => p.Email.Equals(email));

        if(participant == null) return; 
        
        var newParticipant = Participant.Create(participant!.Name,participant.Surname,participant.DateOfBirth,participant.Email,
            participant.Password);
        
        newParticipant.Events.AddRange(participant.Events);

        _unitOfWork.Participants.Delete(participant);

        var _event = _unitOfWork.Events.GetAllAsync().Result.FirstOrDefault(e => e.Id == id);
        
        if(_event == null) return; 
        
        _event!.Participants.Add(newParticipant);
        
        await _unitOfWork.SaveDbAsync();
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task<List<Event>> SearchEvents(string search)
    {
<<<<<<< HEAD
        return await _searchEventsUseCase.ExecuteAsync(search);
    }
}
=======
        var events = await _unitOfWork.Events.GetEventsWithParticipantsAsync();
        var foundEvents = events.Where(e => e.Contains(search)).ToList();
        return foundEvents;
    }
}
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
