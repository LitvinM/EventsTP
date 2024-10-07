using Application.Contracts;
using Core.Models;

namespace Application.Abstraction;

public interface IAdminService
{
    Task DeleteEvent(Guid id);
    Task UpdateEvent(Event entity, Guid id);
    Task AddEvent(CreateEventRequest request);
}