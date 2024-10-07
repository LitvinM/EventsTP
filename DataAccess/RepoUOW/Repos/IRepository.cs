<<<<<<<< HEAD:DataAccess/RepoUOW/Repos/IRepository.cs
namespace DataAccess.RepoUOW.Repos;
========
namespace Core.Abstraction;
>>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79:Core/Abstraction/IRepository.cs

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}