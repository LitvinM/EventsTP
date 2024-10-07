using AutoMapper;
using Core.Models;
using DataAccess.Entities;
using DataAccess.RepoUOW.Repos;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.ParticipantsUseCases;

public class UpdateParticipantUseCase
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;

    public UpdateParticipantUseCase(IParticipantRepository participantRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Guid id, Participant participant)
    {
        if (id != participant.Id)
        {
            throw new BadHttpRequestException("Bad request");
        }

        var participantEntity = _mapper.Map<ParticipantEntity>(participant);
        _participantRepository.Update(participantEntity);
    }
}

