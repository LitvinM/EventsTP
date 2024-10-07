using AutoMapper;
using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.ParticipantsUseCases;

public class GetAllParticipantsUseCase
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;

    public GetAllParticipantsUseCase(IParticipantRepository participantRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Participant>> ExecuteAsync()
    {
        var participantEntities = await _participantRepository.GetAllAsync();
        return participantEntities.Select(pe => _mapper.Map<Participant>(pe));
    }
}
