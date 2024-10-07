using AutoMapper;
using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.ParticipantsUseCases;

public class GetParticipantByIdUseCase
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;

    public GetParticipantByIdUseCase(IParticipantRepository participantRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _mapper = mapper;
    }

    public async Task<Participant> ExecuteAsync(Guid id)
    {
        var participantEntity = await _participantRepository.GetByIdAsync(id);
        if (participantEntity == null)
        {
            throw new KeyNotFoundException("Participant not found");
        }

        return _mapper.Map<Participant>(participantEntity);
    }
}
