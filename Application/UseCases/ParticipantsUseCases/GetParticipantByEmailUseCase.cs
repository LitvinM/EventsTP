using AutoMapper;
using Core.Models;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.ParticipantsUseCases;

public class GetParticipantByEmailUseCase
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;

    public GetParticipantByEmailUseCase(IParticipantRepository participantRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _mapper = mapper;
    }

    public async Task<Participant> ExecuteAsync(string email)
    {
        var participantEntity = await _participantRepository.GetByEmailAsync(email);
        if (participantEntity == null)
        {
            throw new KeyNotFoundException("Participant not found");
        }

        return _mapper.Map<Participant>(participantEntity);
    }
}
