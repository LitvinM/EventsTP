using Application.AdditionalLogic;
using Application.Contracts;
using AutoMapper;
using Core.Models;
using DataAccess.Entities;
using DataAccess.RepoUOW.Repos;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.ParticipantsUseCases;

public class AddParticipantUseCase
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IMapper _mapper;

    public AddParticipantUseCase(IParticipantRepository participantRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(CreateParticipantRequest request)
    {
        var hashedPassword = PasswordHasher.Generate(request.Password);

        var existingParticipants = await _participantRepository.GetAllAsync();
        if (!Checking.CheckParticipantData(request, existingParticipants.Select(p => _mapper.Map<Participant>(p)).ToList()))
        {
            throw new BadHttpRequestException("Bad request");
        }

        request.Password = hashedPassword;
        var participantEntity = _mapper.Map<ParticipantEntity>(request);

        await _participantRepository.AddAsync(participantEntity);
    }
}

