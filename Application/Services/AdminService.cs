<<<<<<< HEAD
using Application.Abstraction;
using Application.Contracts;
using AutoMapper;
using Core.Models;
using DataAccess.Entities;
=======
using AutoMapper;
using Core.Abstraction;
using Core.Contracts;
using Core.Models;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using DataAccess.RepoUOW;

namespace Application.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
<<<<<<< HEAD

    public async Task AddEvent(CreateEventRequest request)
    {
        var _event = _mapper.Map<Event>(request);
        await _unitOfWork.Events.AddAsync( _mapper.Map<EventEntity>(_event));
=======
    
    public async Task AddEvent(CreateEventRequest request)
    {
        var _event = _mapper.Map<Event>(request);
        await _unitOfWork.Events.AddAsync(_event);
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        await _unitOfWork.SaveDbAsync();
    }
    
    public async Task UpdateEvent(Event entity, Guid id)
    {
<<<<<<< HEAD
        _unitOfWork.Events.Update(_mapper.Map<EventEntity>(entity));
=======
        _unitOfWork.Events.Update(entity);
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        await _unitOfWork.SaveDbAsync();
    }
    
    public async Task DeleteEvent(Guid id)
    {
        var eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
        if (eventEntity == null)
        {
            throw new KeyNotFoundException("Not found");
        }
        
        _unitOfWork.Events.Delete(eventEntity);
    
        await _unitOfWork.SaveDbAsync();
    }
}