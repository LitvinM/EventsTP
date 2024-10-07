using Core.Models;
using DataAccess.Entities;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.EventsUseCase;

public class AddParticipantToEventUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly IParticipantRepository _participantRepository;

    public AddParticipantToEventUseCase(IEventRepository eventRepository, IParticipantRepository participantRepository)
    {
        _eventRepository = eventRepository;
        _participantRepository = participantRepository;
    }

    public async Task ExecuteAsync(Guid eventId, string email)
    {
        var participant = await _participantRepository.GetByEmailAsync(email);
        if (participant == null)
        {
            throw new KeyNotFoundException("Participant not found");
        }

        var eventEntity = await _eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException("Event not found");
        }

        var newParticipant = new ParticipantEntity
        {
            Id = participant.Id,
            Name = participant.Name,
            Surname = participant.Surname,
            Email = participant.Email,
            DateOfBirth = participant.DateOfBirth,
            Password = participant.Password,
            Events = participant.Events.ToList()
        };

        eventEntity.Participants.Add(newParticipant);
        _eventRepository.Update(eventEntity);
    }
}

