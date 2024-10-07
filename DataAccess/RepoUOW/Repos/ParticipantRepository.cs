<<<<<<< HEAD
=======
using Core.Abstraction;
using Core.Models;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.RepoUOW.Repos;

<<<<<<< HEAD

public interface IParticipantRepository : IRepository<ParticipantEntity>
{
    Task<ParticipantEntity> GetByEmailAsync(string email);
}

=======
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
public class ParticipantRepository : IParticipantRepository
{
    private readonly DbSet<ParticipantEntity> _dbSet;

    public ParticipantRepository(TaskContext context)
    {
        _dbSet = context.Set<ParticipantEntity>();
    }

<<<<<<< HEAD
    public async Task<IEnumerable<ParticipantEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<ParticipantEntity> GetByIdAsync(Guid id)
    {
        return (await _dbSet
            .Include(p => p.Events)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id))!;
    }

    public async Task<ParticipantEntity> GetByEmailAsync(string email)
    {
        return (await _dbSet
            .Include(p => p.Events)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == email))!;
    }

    public async Task AddAsync(ParticipantEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(ParticipantEntity entity)
    {
=======
    public async Task<IEnumerable<Participant>> GetAllAsync()
    {
        var participantEntities = await _dbSet.AsNoTracking().ToListAsync();
        var participants = participantEntities.Select(p =>
            Participant.Create(p.Name, p.Surname, p.DateOfBirth, p.Email, p.Password));
        return participants;
    }

    public async Task<Participant> GetByIdAsync(Guid id)
    {
        var pe = (await _dbSet
            .Include(p => p.Events)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id))!;
        var participant = 
            Participant.Create(pe.Name, pe.Surname, pe.DateOfBirth, pe.Email, pe.Password);
        return participant;
    }
    
    public async Task<Participant> GetByEmailAsync(string email)
    {
        var pe = (await _dbSet
            .Include(p => p.Events)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == email))!;
        var participant = 
            Participant.Create(pe.Name, pe.Surname, pe.DateOfBirth, pe.Email, pe.Password);
        return participant;
    }

    public async Task<IEnumerable<Participant>> GetParticipantsWithEventsAsync()
    {
        var participantEntities = await _dbSet
            .Include(p => p.Events)
            .AsNoTracking()
            .ToListAsync();
        var participants = participantEntities.Select(p =>
            Participant.Create(p.Name, p.Surname, p.DateOfBirth, p.Email, p.Password));
        return participants;
    }
    public async Task AddAsync(Participant entity)
    {
        var p = new ParticipantEntity()
        {
            Id = entity.Id,
            Name = entity.Name,
            DateOfBirth = entity.DateOfBirth,
            Email = entity.Email,
            Password = entity.Password,
            Surname = entity.Surname,
        };
        await _dbSet.AddAsync(p);
    }

    public void Update(Participant participant)
    {
        var entity = new ParticipantEntity()
        {
            Id = participant.Id,
            Name = participant.Name,
            DateOfBirth = participant.DateOfBirth,
            Email = participant.Email,
            Password = participant.Password,
            Surname = participant.Surname,
        };
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
        var existingEntity = _dbSet
            .Include(p => p.Events)
            .FirstOrDefault(p => p.Id == entity.Id);

        if (existingEntity != null)
        {
            _dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);
<<<<<<< HEAD
=======

>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
            existingEntity.Events.Clear();
            foreach (var evt in entity.Events)
            {
                existingEntity.Events.Add(evt);
            }
        }
        else
        {
            _dbSet.Add(entity);
        }
    }

<<<<<<< HEAD
    public void Delete(ParticipantEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
=======
    public void Delete(Participant participant)
    {
        var entity = new ParticipantEntity()
        {
            Id = participant.Id,
            Name = participant.Name,
            DateOfBirth = participant.DateOfBirth,
            Email = participant.Email,
            Password = participant.Password,
            Surname = participant.Surname,
        };
        _dbSet.Remove(entity);
    }
}
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
