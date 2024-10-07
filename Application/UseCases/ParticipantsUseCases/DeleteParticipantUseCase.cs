using AutoMapper;
using DataAccess.RepoUOW.Repos;

namespace Application.UseCases.ParticipantsUseCases;

public class DeleteParticipantUseCase
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;

    public DeleteParticipantUseCase(IParticipantRepository participantRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var participantEntity = await _participantRepository.GetByIdAsync(id);
        if (participantEntity == null)
        {
            throw new KeyNotFoundException("Participant not found");
        }

        _participantRepository.Delete(participantEntity);
    }
}
