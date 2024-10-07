<<<<<<< HEAD
using Application.Abstraction;
using Application.AdditionalLogic;
using AutoMapper;
using Application.Contracts;
using Application.UseCases.ParticipantsUseCases;
using Core.Models;
=======
using AutoMapper;
using Core.Models;
using Application.AdditionalLogic;
using Core.Abstraction;
using Core.Contracts;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using DataAccess.RepoUOW;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class ParticipantService : IParticipantService
{
<<<<<<< HEAD
    private readonly GetAllParticipantsUseCase _getAllParticipantsUseCase;
    private readonly AddParticipantUseCase _addParticipantUseCase;
    private readonly UpdateParticipantUseCase _updateParticipantUseCase;
    private readonly DeleteParticipantUseCase _deleteParticipantUseCase;
    private readonly GetParticipantByIdUseCase _getParticipantByIdUseCase;
    private readonly GetParticipantByEmailUseCase _getParticipantByEmailUseCase;
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantService(
        GetAllParticipantsUseCase getAllParticipantsUseCase,
        AddParticipantUseCase addParticipantUseCase,
        UpdateParticipantUseCase updateParticipantUseCase,
        DeleteParticipantUseCase deleteParticipantUseCase,
        GetParticipantByIdUseCase getParticipantByIdUseCase,
        GetParticipantByEmailUseCase getParticipantByEmailUseCase,
        IUnitOfWork unitOfWork)
    {
        _getAllParticipantsUseCase = getAllParticipantsUseCase;
        _addParticipantUseCase = addParticipantUseCase;
        _updateParticipantUseCase = updateParticipantUseCase;
        _deleteParticipantUseCase = deleteParticipantUseCase;
        _getParticipantByIdUseCase = getParticipantByIdUseCase;
        _getParticipantByEmailUseCase = getParticipantByEmailUseCase;
        _unitOfWork = unitOfWork;
=======
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task<IEnumerable<Participant>> GetAllParticipants()
    {
<<<<<<< HEAD
        return await _getAllParticipantsUseCase.ExecuteAsync();
=======
        return await _unitOfWork.Participants.GetAllAsync();
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task AddParticipant(CreateParticipantRequest request)
    {
<<<<<<< HEAD
        await _addParticipantUseCase.ExecuteAsync(request);
=======
        var hashedPassword = PasswordHasher.Generate(request.Password);

        if (!Checking.CheckParticipantData(request, _unitOfWork.Participants.GetAllAsync().Result.ToList())) 
            throw new BadHttpRequestException("Bad request");
        
        request.Password = hashedPassword;
        
        var participant = _mapper.Map<Participant>(request);
        
        await _unitOfWork.Participants.AddAsync(participant);
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        await _unitOfWork.SaveDbAsync();
    }

    public async Task UpdateParticipant(Guid id, Participant participant)
    {
<<<<<<< HEAD
        await _updateParticipantUseCase.ExecuteAsync(id, participant);
=======
        if (id != participant.Id)
        {
            throw new BadHttpRequestException("Bad request");
        }

        _unitOfWork.Participants.Update(participant);
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        await _unitOfWork.SaveDbAsync();
    }

    public async Task DeleteParticipant(Guid id)
    {
<<<<<<< HEAD
        await _deleteParticipantUseCase.ExecuteAsync(id);
        await _unitOfWork.SaveDbAsync();
=======
        var participant = await _unitOfWork.Participants.GetByIdAsync(id);
        if (participant == null)
        {
            throw new KeyNotFoundException("Not found");
        }
        
        _unitOfWork.Participants.Delete(participant);
    
        await _unitOfWork.SaveDbAsync();

>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task<Participant> GetParticipantById(Guid id)
    {
<<<<<<< HEAD
        return await _getParticipantByIdUseCase.ExecuteAsync(id);
=======
        var participant = await _unitOfWork.Participants.GetByIdAsync(id);
        if (participant == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        return participant;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
    }

    public async Task<Participant> GetParticipantByEmail(string email)
    {
<<<<<<< HEAD
        return await _getParticipantByEmailUseCase.ExecuteAsync(email);
    }
}
=======
        var participant = await _unitOfWork.Participants.GetByEmailAsync(email);
        if (participant == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        return participant;
    }
}
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
