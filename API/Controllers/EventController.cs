<<<<<<< HEAD
using Application.Abstraction;
=======
using Core.Abstraction;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsTP.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    [HttpGet("wop")]
    public async Task<ActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllEvents();
        
        return Ok(events);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllEventsWithParticipants()
    {
        var events = await _eventService.GetEventsWithParticipantsAsync();
        
        return Ok(events);
    }
    
    
    [HttpGet("search/{search}")]
    public async Task<ActionResult> Search(string search)
    {
        var foundEvents = await _eventService.SearchEvents(search);
        
        return Ok(foundEvents);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetEventById(Guid id)
    {
        var e = await _eventService.GetEventById(id);
        
        return Ok(e);
    }
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult> AddParticipantToEvent([FromBody]string participantEmail, Guid id)
    {
        if (participantEmail == string.Empty) return NoContent();
        await _eventService.AddParticipant(id, participantEmail);
        return NoContent();
    }
}