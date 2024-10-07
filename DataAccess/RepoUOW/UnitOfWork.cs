using AutoMapper;
<<<<<<< HEAD
=======
using Core.Abstraction;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using DataAccess.RepoUOW.Repos;

namespace DataAccess.RepoUOW;

public interface IUnitOfWork
{
    IEventRepository Events { get; }
    IParticipantRepository Participants { get; }
    Task<int> SaveDbAsync();
}
public class UnitOfWork : IUnitOfWork
{
    private readonly TaskContext _context;

    public UnitOfWork(TaskContext context)
    {
        _context = context;
        Events = new EventRepository(context);
        Participants = new ParticipantRepository(context);
    }

    public IEventRepository Events { get; }

    public IParticipantRepository Participants { get; }

    public async Task<int> SaveDbAsync()
    {
        return await _context.SaveChangesAsync();
    }
}