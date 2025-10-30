using HockeyPlatform.Domain.Models;

namespace HockeyPlatform.Domain.Abstractions;

public interface IEventsService
{
    Task<List<EventModel>> GetAllEventsAsync();
    Task<EventModel?> GetEventAsync(int id);
    Task<bool> CreateEventAsync(
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds);
    Task<EventModel?> UpdateEventAsync(
        int id, 
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds);
    Task<bool> DeleteEventAsync(int id);
}