using HockeyPlatform.API.Contracts;
using HockeyPlatform.Domain.Abstractions;
using HockeyPlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HockeyPlatform.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    
    private readonly IEventsRepository _eventsRepository;

    public EventsController(IEventsRepository eventsRepository)
    {
        _eventsRepository = eventsRepository;
    }

    [HttpGet]
    public async Task<List<EventModel>> GetEvents()
    {
        var events = await _eventsRepository.GetAllAsync();
        
        return events;
    }

    [HttpGet("{id}")]
    public async Task<EventModel?> GetEvent(int id)
    {
        var eventModel = await _eventsRepository.GetAsync(id);

        return eventModel;
    }

    [HttpPost]
    public async Task<bool> CreateEvent([FromBody] EventModelRequest request)
    {
        var result = await _eventsRepository.CreateAsync(
            request.Title, 
            request.Description, 
            request.Price, 
            request.DeadlineTime, 
            request.MinCountPlayers, 
            request.PlayerIds);
        
        return result;
    }

    [HttpPut("{id}")]
    public async Task<EventModel?> UpdateEvent(int id, [FromBody] EventModelRequest request)
    {
        var eventModel = await _eventsRepository.UpdateAsync(
            id,
            request.Title, 
            request.Description, 
            request.Price, 
            request.DeadlineTime, 
            request.MinCountPlayers, 
            request.PlayerIds);
        
        return eventModel;
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteEvent(int id)
    {
        var result = await _eventsRepository.DeleteAsync(id);
        
        return result;
    }
}