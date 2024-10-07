using Core.Contracts;
using Core.Models;

namespace Core.Abstraction;

public interface IAdminService
{
    Task DeleteEvent(Guid id);
    Task UpdateEvent(Event entity, Guid id);
    Task AddEvent(CreateEventRequest request);
}