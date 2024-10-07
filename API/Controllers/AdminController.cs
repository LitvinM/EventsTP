<<<<<<< HEAD
using Application.Abstraction;
using Application.Contracts;
=======
using Core.Abstraction;
using Core.Contracts;
>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsTP.Controllers;

[ApiController]
[Route("api/[controller]/event")]
[Authorize(Policy = "AdminPolicy")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    [HttpPost]
    public async Task<ActionResult> AddEvent([FromBody] CreateEventRequest request)
    {
        await _adminService.AddEvent(request);
        
        return Ok("Added");
    }
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEvent([FromBody]Event entity, Guid id)
    {
        await _adminService.UpdateEvent(entity, id);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(Guid id)
    {
        await _adminService.DeleteEvent(id);

        return NoContent();
    }
}