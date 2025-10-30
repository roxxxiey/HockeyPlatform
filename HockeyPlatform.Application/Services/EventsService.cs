using HockeyPlatform.Domain.Abstractions;
using HockeyPlatform.Domain.Models;

namespace HockeyPlatform.Application.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;
    
    public  EventsService(IEventsRepository eventsRepository)
    {
        _eventsRepository = eventsRepository;
    }

    public async Task<List<EventModel>> GetAllEventsAsync()
    {
        return await _eventsRepository.GetAllAsync();
    }

    public async Task<EventModel?> GetEventAsync(int id)
    {
        return await _eventsRepository.GetAsync(id);
    }

    public async Task<bool> CreateEventAsync(
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds)
    {
        return await _eventsRepository.CreateAsync(title, description, price, 
            deadlineTime, minCountPlayers, playerIds);
    }

    public async Task<EventModel?> UpdateEventAsync(
        int id, 
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds)
    {
        return await _eventsRepository.UpdateAsync(id, title, description, price, 
            deadlineTime, minCountPlayers, playerIds);
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        return await _eventsRepository.DeleteAsync(id);
    }
}