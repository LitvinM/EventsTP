<<<<<<< HEAD
using Core.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.RepoUOW.Repos;

public interface IEventRepository : IRepository<EventEntity>
{
    Task<IEnumerable<EventEntity>> GetEventsWithParticipantsAsync();
}
=======
using AutoMapper;
using Core.Abstraction;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DataAccess.RepoUOW.Repos;

>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79

public class EventRepository : IEventRepository
{
    private readonly DbSet<EventEntity> _dbSet;

    public EventRepository(TaskContext context)
    {
        _dbSet = context.Set<EventEntity>();
    }

<<<<<<< HEAD
    public async Task<IEnumerable<EventEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<EventEntity> GetByIdAsync(Guid id)
    {
        return (await _dbSet.Include(e => e.Participants)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id))!;
    }

    public async Task<IEnumerable<EventEntity>> GetEventsWithParticipantsAsync()
    {
        return await _dbSet.Include(e => e.Participants).AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(EventEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(EventEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(EventEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
=======
    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        var eventsEntities = await _dbSet.AsNoTracking().ToListAsync();
        
        var events = eventsEntities.Select(e =>
            Event.Create(e.Name, e.Description, e.TimeAndDate, e.Place, e.Category, e.ParticipantsMaxAmount, e.Image));
        
        return events;
    }

    public async Task<Event> GetByIdAsync(Guid id)
    {
        var eventEntity = 
            (await _dbSet.Include(e => e.Participants).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id))!;
        return Event.Create(eventEntity.Name, eventEntity.Description, eventEntity.TimeAndDate,
            eventEntity.Place, eventEntity.Category, eventEntity.ParticipantsMaxAmount, eventEntity.Image);
    }

    public async Task<IEnumerable<Event>> GetEventsWithParticipantsAsync()
    {
        var eventsEntities = await _dbSet.Include(e => e.Participants).AsNoTracking().ToListAsync();
        var events = eventsEntities.Select(e =>
            Event.Create(e.Name, e.Description, e.TimeAndDate, e.Place, e.Category, e.ParticipantsMaxAmount, e.Image));
        return events;
    }
    public async Task AddAsync(Event entity)
    {
        var participantsEntities = entity.Participants.Select(p => new ParticipantEntity()
        {
            Id = p.Id,
            Name = p.Name,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            Password = p.Password,
            Surname = p.Surname,
        }).ToList();
        var e = new EventEntity()
        {
            Id = entity.Id,
            Category = entity.Category,
            Description = entity.Description,
            Image = entity.Image,
            Name = entity.Name,
            Place = entity.Place,
            ParticipantsMaxAmount = entity.ParticipantsMaxAmount,
            TimeAndDate = entity.TimeAndDate,
            Participants = participantsEntities
        };
        await _dbSet.AddAsync(e);
    }

    public void Update(Event entity)
    {
        var existingEntity = _dbSet.FirstOrDefault(e => e.Id == entity.Id);

        if (existingEntity == null) return;
        
        existingEntity.Category = entity.Category;
        existingEntity.Description = entity.Description;
        existingEntity.Name = entity.Name;
        existingEntity.Place = entity.Place;
        existingEntity.Image = entity.Image;
        existingEntity.ParticipantsMaxAmount = entity.ParticipantsMaxAmount;
        existingEntity.TimeAndDate = entity.TimeAndDate;

    }

    public void Delete(Event entity)
    {
        var participantsEntities = entity.Participants.Select(p => new ParticipantEntity()
        {
            Id = p.Id,
            Name = p.Name,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            Password = p.Password,
            Surname = p.Surname,
        }).ToList();
        var e = new EventEntity()
        {
            Id = entity.Id,
            Category = entity.Category,
            Description = entity.Description,
            Image = entity.Image,
            Name = entity.Name,
            Place = entity.Place,
            ParticipantsMaxAmount = entity.ParticipantsMaxAmount,
            TimeAndDate = entity.TimeAndDate,
            Participants = participantsEntities
        };
        _dbSet.Remove(e);
    }
}
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
