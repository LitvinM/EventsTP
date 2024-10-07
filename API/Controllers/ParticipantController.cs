<<<<<<< HEAD
using Application.Abstraction;
using Application.Contracts;
=======
using Core.Abstraction;
using Core.Contracts;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsTP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantController(IParticipantService participantService)
    {
        _participantService = participantService;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllParticipants()
    {
        var participants = await _participantService.GetAllParticipants();
        
        return Ok(participants);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddParticipant([FromBody] CreateParticipantRequest request)
    {
        await _participantService.AddParticipant(request);

        return Ok("Added");
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateParticipant(Guid id, Participant participant)
    {
        await _participantService.UpdateParticipant(id, participant);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteParticipant(Guid id)
    {
        await _participantService.DeleteParticipant(id);
        
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetParticipantById(Guid id)
    {
        var participant = await _participantService.GetParticipantById(id);
        return Ok(participant);
    }
    
    
    [HttpPost("byemail")]
    public async Task<ActionResult> GetParticipantByEmail([FromBody]string email)
    {
        var participant = await _participantService.GetParticipantByEmail(email);
        return Ok(participant);
    }
}