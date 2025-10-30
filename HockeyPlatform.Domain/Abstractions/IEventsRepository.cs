using HockeyPlatform.Domain.Models;

namespace HockeyPlatform.Domain.Abstractions;

public interface IEventsRepository
{
    Task<List<EventModel>> GetAllAsync();
    Task<EventModel?> GetAsync(int id);
    Task<bool> CreateAsync(
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds);
    Task<EventModel?> UpdateAsync(
        int id, 
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds);
    Task<bool> DeleteAsync(int id);
}